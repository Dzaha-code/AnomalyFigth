using System;
using System.Collections.Generic;
using WinFormsApp1.Models;
using WinFormsApp1.DataAccess;

namespace WinFormsApp1.Logic
{
    /// <summary>
    /// BattleManager.cs - Mengelola semua logika battle system.
    /// Kelas ini non-visual dan hanya fokus pada game logic.
    /// Tidak ada UI di sini, hanya kalkulasi dan state management.
    /// </summary>
    public class BattleManager
    {
        // ===== STATE GAME =====

        // HP saat ini kedua pemain
        public int HpPlayer1 { get; private set; }
        public int HpPlayer2 { get; private set; }

        // Maksimal HP (awal battle, untuk reference)
        private int MaxHpP1;
        private int MaxHpP2;

        // Current turn: 1 = Player 1's turn, 2 = Player 2's turn
        public int CurrentTurn { get; private set; }

        // Apakah pemain defend turn ini? (damage incoming dikurangi 50%)
        public bool IsDefendingP1 { get; private set; }
        public bool IsDefendingP2 { get; private set; }

        // Skill yang dimiliki tiap player (di-load dari SkillRepository saat init)
        public List<Skill> SkillsP1 { get; private set; }
        public List<Skill> SkillsP2 { get; private set; }

        // Cooldown tracker: Dictionary<skillId, turnsRemaining>
        // Jika skillId tidak ada di dict atau value = 0, skill siap pakai
        public Dictionary<int, int> CooldownP1 { get; private set; }
        public Dictionary<int, int> CooldownP2 { get; private set; }

        // Cooldown item: Dictionary<itemIndex, turnsRemaining>
        // Hardcode cooldown item = 2 turn (tidak dari database)
        public Dictionary<int, int> ItemCooldownP1 { get; private set; }
        public Dictionary<int, int> ItemCooldownP2 { get; private set; }

        // Boost sementara (dalam satuan turn)
        // Contoh: AttackBoostP1 = 2 berarti sisa 2 turn lagi attack +5 (misalnya)
        public int AttackBoostP1 { get; private set; }
        public int AttackBoostP2 { get; private set; }
        public int DefenseBoostP1 { get; private set; }
        public int DefenseBoostP2 { get; private set; }

        // Nilai boost sebenarnya (ditambahkan ke attack/defense)
        private int AttackBoostValueP1;
        private int AttackBoostValueP2;
        private int DefenseBoostValueP1;
        private int DefenseBoostValueP2;

        // Referensi data pemain & anomaly dari GameSession
        private Player PlayerP1;
        private Player PlayerP2;
        private Anomaly AnomalyP1;
        private Anomaly AnomalyP2;
        private List<Item> ItemsP1;
        private List<Item> ItemsP2;

        // Konstanta cooldown item (hardcode sesuai spec)
        private const int ITEM_COOLDOWN_TURNS = 2;

        // ===== CONSTRUCTOR & INIT =====

        public BattleManager()
        {
            // Inisialisasi collections
            SkillsP1 = new List<Skill>();
            SkillsP2 = new List<Skill>();
            CooldownP1 = new Dictionary<int, int>();
            CooldownP2 = new Dictionary<int, int>();
            ItemCooldownP1 = new Dictionary<int, int>();
            ItemCooldownP2 = new Dictionary<int, int>();
        }

        /// <summary>
        /// Inisialisasi battle dari GameSession.
        /// Harus dipanggil sebelum battle dimulai!
        /// </summary>
        public void InitBattle()
        {
            // Load data pemain & anomaly dari GameSession
            PlayerP1 = GameSession.Player1;
            PlayerP2 = GameSession.Player2;
            AnomalyP1 = GameSession.Player1Anomaly;
            AnomalyP2 = GameSession.Player2Anomaly;
            ItemsP1 = GameSession.Player1Items;
            ItemsP2 = GameSession.Player2Items;

            // Inisialisasi HP dari base HP anomaly
            MaxHpP1 = AnomalyP1.BaseHP;
            MaxHpP2 = AnomalyP2.BaseHP;
            HpPlayer1 = MaxHpP1;
            HpPlayer2 = MaxHpP2;

            // Player 1 mulai duluan
            CurrentTurn = 1;

            // Load skill untuk tiap anomaly
            SkillRepository skillRepo = new SkillRepository();
            SkillsP1 = skillRepo.GetSkillsByAnomalyId(AnomalyP1.AnomalyID);
            SkillsP2 = skillRepo.GetSkillsByAnomalyId(AnomalyP2.AnomalyID);

            // Inisialisasi cooldown dict untuk semua skill (all = 0, artinya ready)
            foreach (Skill skill in SkillsP1)
                CooldownP1[skill.SkillID] = 0;

            foreach (Skill skill in SkillsP2)
                CooldownP2[skill.SkillID] = 0;

            // Inisialisasi item cooldown (semua item siap awal battle)
            for (int i = 0; i < ItemsP1.Count; i++)
                ItemCooldownP1[i] = 0;

            for (int i = 0; i < ItemsP2.Count; i++)
                ItemCooldownP2[i] = 0;

            // Reset defend & boost
            IsDefendingP1 = false;
            IsDefendingP2 = false;
            AttackBoostP1 = 0;
            AttackBoostP2 = 0;
            DefenseBoostP1 = 0;
            DefenseBoostP2 = 0;
            AttackBoostValueP1 = 0;
            AttackBoostValueP2 = 0;
            DefenseBoostValueP1 = 0;
            DefenseBoostValueP2 = 0;
        }

        // ===== AKSI BATTLE =====

        /// <summary>
        /// Aksi ATTACK - Attacker menyerang Defender.
        /// Damage = Attacker.Attack - Defender.Defense (minimal 1)
        /// Jika defender menggunakan Defend, damage dikurangi 50%.
        /// Return: string deskripsi hasil damage.
        /// </summary>
        public string DoAttack()
        {
            int attackerAttack = GetEffectiveAttack(CurrentTurn);
            int defenderDefense = GetEffectiveDefense(CurrentTurn == 1 ? 2 : 1);

            int baseDamage = attackerAttack - defenderDefense;
            if (baseDamage < 1) baseDamage = 1;

            // Cek apakah defender menggunakan Defend
            bool defending = (CurrentTurn == 1) ? IsDefendingP2 : IsDefendingP1;
            if (defending)
            {
                baseDamage = baseDamage / 2; // Divide by 2 dengan pembulatan ke bawah
            }

            // Apply damage
            if (CurrentTurn == 1)
                HpPlayer2 -= baseDamage;
            else
                HpPlayer1 -= baseDamage;

            // Pastikan HP tidak negatif
            if (HpPlayer1 < 0) HpPlayer1 = 0;
            if (HpPlayer2 < 0) HpPlayer2 = 0;

            string result = GetCurrentPlayerName() + " menyerang dengan serangan biasa!\n";
            result += "Damage: " + baseDamage;
            if (defending) result += " (dikurangi 50% karena lawan defend)";

            NextTurn();
            return result;
        }

        /// <summary>
        /// Aksi SKILL - Gunakan skill dengan index tertentu.
        /// skillIndex = index di SkillsP1 atau SkillsP2.
        /// Skill damage tidak tergantung defense, langsung apply damage.
        /// Return: string deskripsi hasil. Jika skill cooldown, return error message.
        /// </summary>
        public string DoSkill(int skillIndex)
        {
            List<Skill> skills = (CurrentTurn == 1) ? SkillsP1 : SkillsP2;
            Dictionary<int, int> cooldown = (CurrentTurn == 1) ? CooldownP1 : CooldownP2;

            if (skillIndex < 0 || skillIndex >= skills.Count)
                return "Skill index tidak valid!";

            Skill skill = skills[skillIndex];

            // Cek apakah skill dalam cooldown
            if (cooldown.ContainsKey(skill.SkillID) && cooldown[skill.SkillID] > 0)
                return skill.Name + " masih cooldown! Tunggu " + cooldown[skill.SkillID] + " turn.";

            // Cek apakah defender menggunakan Defend (skill abaikan defense, tapi damage masih bisa dikurangi defend)
            int damage = skill.Damage;
            bool defending = (CurrentTurn == 1) ? IsDefendingP2 : IsDefendingP1;
            if (defending)
            {
                damage = damage / 2;
            }

            // Apply damage
            if (CurrentTurn == 1)
                HpPlayer2 -= damage;
            else
                HpPlayer1 -= damage;

            if (HpPlayer1 < 0) HpPlayer1 = 0;
            if (HpPlayer2 < 0) HpPlayer2 = 0;

            // Set cooldown untuk skill ini (MpCost dijadikan cooldown turn count)
            cooldown[skill.SkillID] = skill.MpCost;

            string result = GetCurrentPlayerName() + " menggunakan skill " + skill.Name + "!\n";
            result += "Damage: " + damage + "\n";
            result += "Cooldown: " + skill.MpCost + " turn";

            NextTurn();
            return result;
        }

        /// <summary>
        /// Aksi DEFEND - Kurangi incoming damage sebesar 50% turn ini.
        /// Setelah turn berakhir, status defend hilang.
        /// Return: string deskripsi.
        /// </summary>
        public string DoDefend()
        {
            if (CurrentTurn == 1)
                IsDefendingP1 = true;
            else
                IsDefendingP2 = true;

            string result = GetCurrentPlayerName() + " mengambil stance pertahanan!\n";
            result += "Incoming damage turn ini akan dikurangi 50%.";

            NextTurn();
            return result;
        }

        /// <summary>
        /// Aksi ITEM - Gunakan item dengan index tertentu.
        /// itemIndex = index di ItemsP1 atau ItemsP2.
        /// Efek item:
        ///   "heal" → tambah HP sebesar EffectValue (max = MaxHp)
        ///   "attack_boost" → tambah Attack sebesar EffectValue, valid 2 turn
        ///   "defense_boost" → tambah Defense sebesar EffectValue, valid 2 turn
        /// Return: string deskripsi hasil. Jika item cooldown, return error.
        /// </summary>
        public string DoItem(int itemIndex)
        {
            List<Item> items = (CurrentTurn == 1) ? ItemsP1 : ItemsP2;
            Dictionary<int, int> cooldown = (CurrentTurn == 1) ? ItemCooldownP1 : ItemCooldownP2;

            if (itemIndex < 0 || itemIndex >= items.Count)
                return "Item index tidak valid!";

            Item item = items[itemIndex];

            // Cek cooldown
            if (cooldown.ContainsKey(itemIndex) && cooldown[itemIndex] > 0)
                return item.Name + " masih cooldown! Tunggu " + cooldown[itemIndex] + " turn.";

            string result = GetCurrentPlayerName() + " menggunakan item " + item.Name + "!\n";

            // Apply efek berdasarkan EffectType
            if (item.EffectType == "heal")
            {
                int healAmount = item.EffectValue;
                int maxHp = (CurrentTurn == 1) ? MaxHpP1 : MaxHpP2;
                int currentHp = (CurrentTurn == 1) ? HpPlayer1 : HpPlayer2;

                if (currentHp + healAmount > maxHp)
                    healAmount = maxHp - currentHp;

                if (CurrentTurn == 1)
                    HpPlayer1 += healAmount;
                else
                    HpPlayer2 += healAmount;

                result += "Heal: +" + healAmount + " HP";
            }
            else if (item.EffectType == "attack_boost")
            {
                if (CurrentTurn == 1)
                {
                    AttackBoostP1 = 2; // 2 turn boost
                    AttackBoostValueP1 = item.EffectValue;
                }
                else
                {
                    AttackBoostP2 = 2;
                    AttackBoostValueP2 = item.EffectValue;
                }
                result += "Attack Boost: +" + item.EffectValue + " ATK selama 2 turn";
            }
            else if (item.EffectType == "defense_boost")
            {
                if (CurrentTurn == 1)
                {
                    DefenseBoostP1 = 2;
                    DefenseBoostValueP1 = item.EffectValue;
                }
                else
                {
                    DefenseBoostP2 = 2;
                    DefenseBoostValueP2 = item.EffectValue;
                }
                result += "Defense Boost: +" + item.EffectValue + " DEF selama 2 turn";
            }

            // Set item cooldown (hardcode 2 turn)
            cooldown[itemIndex] = ITEM_COOLDOWN_TURNS;

            NextTurn();
            return result;
        }

        // ===== HELPER METHODS =====

        /// <summary>
        /// Cek apakah battle sudah selesai.
        /// Return: 0 = belum selesai, 1 = Player 1 menang, 2 = Player 2 menang
        /// </summary>
        public int CheckWinner()
        {
            if (HpPlayer1 <= 0)
                return 2; // Player 2 menang
            if (HpPlayer2 <= 0)
                return 1; // Player 1 menang
            return 0; // Belum ada pemenang
        }

        /// <summary>
        /// Pindah giliran ke pemain selanjutnya.
        /// Update cooldown skill & item, reset status defend, decrement boost duration.
        /// Called di akhir setiap aksi.
        /// </summary>
        private void NextTurn()
        {
            // Reset defend status
            IsDefendingP1 = false;
            IsDefendingP2 = false;

            // Pindah giliran
            CurrentTurn = (CurrentTurn == 1) ? 2 : 1;

            // Update cooldown skill untuk pemain yang sekarang giliran
            Dictionary<int, int> currentCooldown = (CurrentTurn == 1) ? CooldownP1 : CooldownP2;
            List<int> skillIds = new List<int>(currentCooldown.Keys);
            foreach (int skillId in skillIds)
            {
                if (currentCooldown[skillId] > 0)
                    currentCooldown[skillId]--;
            }

            // Update cooldown item untuk pemain yang sekarang giliran
            Dictionary<int, int> currentItemCooldown = (CurrentTurn == 1) ? ItemCooldownP1 : ItemCooldownP2;
            List<int> itemIndices = new List<int>(currentItemCooldown.Keys);
            foreach (int itemIdx in itemIndices)
            {
                if (currentItemCooldown[itemIdx] > 0)
                    currentItemCooldown[itemIdx]--;
            }

            // Decrement boost duration untuk pemain yang sekarang giliran
            if (CurrentTurn == 1)
            {
                if (AttackBoostP1 > 0) AttackBoostP1--;
                if (DefenseBoostP1 > 0) DefenseBoostP1--;
            }
            else
            {
                if (AttackBoostP2 > 0) AttackBoostP2--;
                if (DefenseBoostP2 > 0) DefenseBoostP2--;
            }
        }

        /// <summary>
        /// Hitung effective attack = base attack + boost (jika boost aktif).
        /// playerNum: 1 = Player 1, 2 = Player 2
        /// </summary>
        private int GetEffectiveAttack(int playerNum)
        {
            int baseAttack = (playerNum == 1) ? AnomalyP1.BaseATK : AnomalyP2.BaseATK;
            int boost = (playerNum == 1) ? AttackBoostValueP1 : AttackBoostValueP2;
            int boostDuration = (playerNum == 1) ? AttackBoostP1 : AttackBoostP2;

            if (boostDuration > 0)
                return baseAttack + boost;
            return baseAttack;
        }

        /// <summary>
        /// Hitung effective defense = base defense + boost (jika boost aktif).
        /// </summary>
        private int GetEffectiveDefense(int playerNum)
        {
            int baseDefense = (playerNum == 1) ? AnomalyP1.BaseDEF : AnomalyP2.BaseDEF;
            int boost = (playerNum == 1) ? DefenseBoostValueP1 : DefenseBoostValueP2;
            int boostDuration = (playerNum == 1) ? DefenseBoostP1 : DefenseBoostP2;

            if (boostDuration > 0)
                return baseDefense + boost;
            return baseDefense;
        }

        /// <summary>
        /// Return nama pemain yang sedang giliran.
        /// </summary>
        private string GetCurrentPlayerName()
        {
            return (CurrentTurn == 1) ? PlayerP1.PlayerName : PlayerP2.PlayerName;
        }

        /// <summary>
        /// Get nama pemain berdasarkan player number.
        /// </summary>
        public string GetPlayerName(int playerNum)
        {
            return (playerNum == 1) ? PlayerP1.PlayerName : PlayerP2.PlayerName;
        }

        /// <summary>
        /// Check apakah skill dengan index tertentu sedang cooldown.
        /// </summary>
        public bool IsSkillOnCooldown(int playerNum, int skillIndex)
        {
            List<Skill> skills = (playerNum == 1) ? SkillsP1 : SkillsP2;
            Dictionary<int, int> cooldown = (playerNum == 1) ? CooldownP1 : CooldownP2;

            if (skillIndex < 0 || skillIndex >= skills.Count)
                return false;

            Skill skill = skills[skillIndex];
            return cooldown.ContainsKey(skill.SkillID) && cooldown[skill.SkillID] > 0;
        }

        /// <summary>
        /// Get cooldown turns remaining untuk skill.
        /// </summary>
        public int GetSkillCooldownRemaining(int playerNum, int skillIndex)
        {
            List<Skill> skills = (playerNum == 1) ? SkillsP1 : SkillsP2;
            Dictionary<int, int> cooldown = (playerNum == 1) ? CooldownP1 : CooldownP2;

            if (skillIndex < 0 || skillIndex >= skills.Count)
                return 0;

            Skill skill = skills[skillIndex];
            if (cooldown.ContainsKey(skill.SkillID))
                return cooldown[skill.SkillID];
            return 0;
        }

        /// <summary>
        /// Check apakah item dengan index tertentu sedang cooldown.
        /// </summary>
        public bool IsItemOnCooldown(int playerNum, int itemIndex)
        {
            Dictionary<int, int> cooldown = (playerNum == 1) ? ItemCooldownP1 : ItemCooldownP2;
            return cooldown.ContainsKey(itemIndex) && cooldown[itemIndex] > 0;
        }

        /// <summary>
        /// Get cooldown turns remaining untuk item.
        /// </summary>
        public int GetItemCooldownRemaining(int playerNum, int itemIndex)
        {
            Dictionary<int, int> cooldown = (playerNum == 1) ? ItemCooldownP1 : ItemCooldownP2;
            if (cooldown.ContainsKey(itemIndex))
                return cooldown[itemIndex];
            return 0;
        }

        /// <summary>
        /// Get HP persentase (0-100) untuk display progress bar.
        /// </summary>
        public int GetHpPercentage(int playerNum)
        {
            int currentHp = (playerNum == 1) ? HpPlayer1 : HpPlayer2;
            int maxHp = (playerNum == 1) ? MaxHpP1 : MaxHpP2;
            return (currentHp * 100) / maxHp;
        }
    }
}
