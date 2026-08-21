using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Models;

namespace WinFormsApp1.Logic
{
    // ===== ENUMS =====

    /// <summary>
    /// State machine fase battle.
    /// WaitingInput → ResolvingAction → CheckWinCondition → TurnEnd → WaitingInput
    /// </summary>
    public enum BattlePhase
    {
        WaitingInput,
        ResolvingAction,
        CheckWinCondition,
        TurnEnd,
        BattleOver
    }

    // ===== FIGHTER STATE =====

    /// <summary>
    /// FighterState — Menyimpan semua data satu pemain selama battle.
    /// Termasuk HP, stats efektif (sudah termasuk item modifier),
    /// skill cooldowns, item cooldowns, status effects, dan defend state.
    /// </summary>
    public class FighterState
    {
        // Data dasar
        public Anomaly Anomaly { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Item> Items { get; set; } = new List<Item>();
        public string PlayerName { get; set; } = "";

        // HP
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }

        // Stats efektif (base + item modifier)
        public int EffectiveATK { get; set; }
        public int EffectiveDEF { get; set; }
        public int EffectiveSPD { get; set; }
        public int RegenPerTurn { get; set; }

        // Item modifiers description untuk log / UI
        public List<string> AppliedItemDescriptions { get; set; } = new List<string>();

        // Defend stance — aktif setelah memilih Defend, berlaku untuk 1 serangan lawan berikutnya
        public bool IsDefending { get; set; }

        // Cooldowns
        public Dictionary<int, int> SkillCooldowns { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> ItemCooldowns { get; set; } = new Dictionary<int, int>();

        // ===== STATUS EFFECTS =====
        public bool HasPoison { get; set; }
        public int PoisonDamage { get; set; }
        public int PoisonTurns { get; set; }

        public bool HasStun { get; set; }
        public bool WasStunnedLastTurn { get; set; } // Anti stun berturut-turut

        public int ShieldHP { get; set; }

        public int ATKBuffValue { get; set; }
        public int ATKBuffTurns { get; set; }

        public int DEFBuffValue { get; set; }
        public int DEFBuffTurns { get; set; }

        // ===== TRACKING STATISTIK =====
        public int TotalDamageDealt { get; set; }
        public Dictionary<string, int> SkillUsageCount { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Inisialisasi fighter dari data Anomaly + Item modifier.
        /// Item pasif diterapkan langsung di sini.
        /// </summary>
        public FighterState(Anomaly anomaly, List<Skill> skills, List<Item> items, string playerName)
        {
            Anomaly = anomaly;
            Skills = skills ?? new List<Skill>();
            Items = items ?? new List<Item>();
            PlayerName = playerName;

            // Base stats
            MaxHP = anomaly.BaseHP;
            CurrentHP = anomaly.BaseHP;
            EffectiveATK = anomaly.BaseATK;
            EffectiveDEF = anomaly.BaseDEF;
            EffectiveSPD = anomaly.BaseSPD;
            RegenPerTurn = 0;

            // Apply item modifier pasif (diterapkan sekali di awal battle)
            foreach (Item item in Items)
            {
                ApplyPassiveItem(item);
            }

            // Inisialisasi cooldown semua skill ke 0 (ready)
            for (int i = 0; i < Skills.Count; i++)
            {
                SkillCooldowns[i] = 0;
            }

            // Inisialisasi cooldown item ke 0
            for (int i = 0; i < Items.Count; i++)
            {
                ItemCooldowns[i] = 0;
            }
        }

        private void ApplyPassiveItem(Item item)
        {
            if (item == null) return;

            string effect = item.EffectType.Trim().ToUpperInvariant();
            double val = item.EffectValue;
            bool isPercent = item.IsPercentage;

            switch (effect)
            {
                case "HP":
                case "HP_BOOST":
                case "HEAL":
                    int hpBonus = isPercent ? (int)Math.Round(Anomaly.BaseHP * (val / 100.0)) : (int)Math.Round(val);
                    MaxHP += hpBonus;
                    CurrentHP += hpBonus;
                    AppliedItemDescriptions.Add($"{item.Name} (+{hpBonus} HP)");
                    break;

                case "ATK":
                case "ATTACK_BOOST":
                    int atkBonus = isPercent ? (int)Math.Round(Anomaly.BaseATK * (val / 100.0)) : (int)Math.Round(val);
                    EffectiveATK += atkBonus;
                    AppliedItemDescriptions.Add($"{item.Name} (+{atkBonus} ATK)");
                    break;

                case "DEF":
                case "DEFENSE_BOOST":
                    int defBonus = isPercent ? (int)Math.Round(Anomaly.BaseDEF * (val / 100.0)) : (int)Math.Round(val);
                    EffectiveDEF += defBonus;
                    AppliedItemDescriptions.Add($"{item.Name} (+{defBonus} DEF)");
                    break;

                case "SPD":
                    int spdBonus = isPercent ? (int)Math.Round(Anomaly.BaseSPD * (val / 100.0)) : (int)Math.Round(val);
                    EffectiveSPD += spdBonus;
                    AppliedItemDescriptions.Add($"{item.Name} (+{spdBonus} SPD)");
                    break;

                case "REGEN":
                    int regenVal = isPercent ? (int)Math.Round(Anomaly.BaseHP * (val / 100.0)) : (int)Math.Round(val);
                    RegenPerTurn += Math.Max(1, regenVal);
                    AppliedItemDescriptions.Add($"{item.Name} (Regen +{regenVal} HP/turn)");
                    break;

                default:
                    AppliedItemDescriptions.Add($"{item.Name} ({item.EffectType})");
                    break;
            }
        }

        public int GetCurrentATK()
        {
            int atk = EffectiveATK;
            if (ATKBuffTurns > 0)
                atk += ATKBuffValue;
            return Math.Max(1, atk);
        }

        public int GetCurrentDEF()
        {
            int def = EffectiveDEF;
            if (DEFBuffTurns > 0)
                def += DEFBuffValue;
            return Math.Max(0, def);
        }
    }

    // ===== BATTLE ENGINE =====

    /// <summary>
    /// BattleEngine.cs — Logika murni battle system.
    /// Mengelola FighterState, damage calculation, skills, items, dan state machine.
    /// </summary>
    public class BattleEngine
    {
        // ===== STATE =====
        public FighterState Fighter1 { get; private set; } = null!;
        public FighterState Fighter2 { get; private set; } = null!;
        public BattlePhase CurrentPhase { get; private set; }
        public int CurrentTurn { get; private set; } // 1 atau 2
        public int TurnNumber { get; private set; }  // Total giliran

        // ===== EVENTS (untuk notify UI) =====
        public event Action<string>? OnBattleLog;
        public event Action<int, int, int>? OnHPChanged;      // playerNum, newHP, maxHP
        public event Action<int>? OnTurnChanged;               // playerNum
        public event Action<int, string>? OnBattleEnd;         // winner, summary
        public event Action? OnPhaseChanged;
        public event Action<int>? OnStunSkipTurn;

        // ===== INIT =====

        public void InitBattle(
            Anomaly anomaly1, List<Skill> skills1, List<Item> items1, string player1Name,
            Anomaly anomaly2, List<Skill> skills2, List<Item> items2, string player2Name)
        {
            Fighter1 = new FighterState(anomaly1, skills1, items1, player1Name);
            Fighter2 = new FighterState(anomaly2, skills2, items2, player2Name);

            // Giliran pertama ditentukan secara random (50:50) atau berdasarkan SPD
            Random rng = new Random();
            CurrentTurn = rng.Next(1, 3);
            TurnNumber = 1;
            CurrentPhase = BattlePhase.WaitingInput;

            // Battle Start Log & Item Detection Summary
            OnBattleLog?.Invoke("=== ANOMALY VERSUS - BATTLE START! ===");
            LogFighterInfo(Fighter1);
            OnBattleLog?.Invoke("");
            LogFighterInfo(Fighter2);
            OnBattleLog?.Invoke("");

            // Mulai giliran pertama
            ProcessTurnStart();
        }

        private void LogFighterInfo(FighterState f)
        {
            OnBattleLog?.Invoke($"👤 {f.PlayerName} menggunakan {f.Anomaly.Name} ({f.Anomaly.Role})");
            if (f.AppliedItemDescriptions.Count > 0)
            {
                OnBattleLog?.Invoke($"   🎒 Item Terpasang: {string.Join(", ", f.AppliedItemDescriptions)}");
            }
            else
            {
                OnBattleLog?.Invoke("   🎒 Item: (Tidak ada item)");
            }
            OnBattleLog?.Invoke($"   📊 Stats Aktif -> HP: {f.MaxHP} | ATK: {f.EffectiveATK} | DEF: {f.EffectiveDEF} | SPD: {f.EffectiveSPD}");
        }

        private FighterState GetCurrentFighter() => (CurrentTurn == 1) ? Fighter1 : Fighter2;
        private FighterState GetOpponentFighter() => (CurrentTurn == 1) ? Fighter2 : Fighter1;
        private int GetOpponentNum() => (CurrentTurn == 1) ? 2 : 1;

        // ===== AKSI BATTLE =====

        /// <summary>
        /// Aksi ATTACK — serangan dasar. Power_Skill = 1.0, tanpa cooldown.
        /// Formula: Damage = (ATK * 1.0) - (DEF_lawan * 0.5), min 1
        /// </summary>
        public bool DoAttack()
        {
            if (CurrentPhase != BattlePhase.WaitingInput) return false;
            var attacker = GetCurrentFighter();
            if (attacker.HasStun) return false;

            CurrentPhase = BattlePhase.ResolvingAction;
            OnPhaseChanged?.Invoke();

            var defender = GetOpponentFighter();
            int damage = CalculateDamage(attacker.GetCurrentATK(), 1.0, defender.GetCurrentDEF(), defender.IsDefending);

            ApplyDamage(defender, damage, GetOpponentNum());
            attacker.TotalDamageDealt += damage;

            OnBattleLog?.Invoke($"⚔ {attacker.PlayerName} menyerang!");
            OnBattleLog?.Invoke($"  💥 Damage: {damage}" + (defender.IsDefending ? " (dikurangi 50% - Defend)" : ""));

            defender.IsDefending = false;
            ProcessPostAction();
            return true;
        }

        /// <summary>
        /// Aksi SKILL — gunakan skill berdasarkan index.
        /// Formula: Damage = (ATK * Power) - (DEF_lawan * 0.5), min 1
        /// </summary>
        public bool DoSkill(int skillIndex)
        {
            if (CurrentPhase != BattlePhase.WaitingInput) return false;
            var attacker = GetCurrentFighter();
            if (attacker.HasStun) return false;

            if (skillIndex < 0 || skillIndex >= attacker.Skills.Count)
            {
                OnBattleLog?.Invoke("❌ Skill index tidak valid!");
                return false;
            }

            if (attacker.SkillCooldowns.ContainsKey(skillIndex) && attacker.SkillCooldowns[skillIndex] > 0)
            {
                OnBattleLog?.Invoke($"❌ {attacker.Skills[skillIndex].Name} masih cooldown ({attacker.SkillCooldowns[skillIndex]} turn)!");
                return false;
            }

            CurrentPhase = BattlePhase.ResolvingAction;
            OnPhaseChanged?.Invoke();

            Skill skill = attacker.Skills[skillIndex];
            var defender = GetOpponentFighter();

            // Track usage
            if (!attacker.SkillUsageCount.ContainsKey(skill.Name))
                attacker.SkillUsageCount[skill.Name] = 0;
            attacker.SkillUsageCount[skill.Name]++;

            string skillType = skill.SkillType.Trim().ToLowerInvariant();

            switch (skillType)
            {
                case "damage":
                    ProcessDamageSkill(attacker, defender, skill);
                    break;
                case "poison":
                    ProcessPoisonSkill(attacker, defender, skill);
                    break;
                case "stun":
                    ProcessStunSkill(attacker, defender, skill);
                    break;
                case "shield":
                    ProcessShieldSkill(attacker, skill);
                    break;
                case "heal":
                    ProcessHealSkill(attacker, skill);
                    break;
                case "buff":
                case "buff_atk":
                    ProcessBuffSkill(attacker, skill, isATK: true);
                    break;
                case "buff_def":
                    ProcessBuffSkill(attacker, skill, isATK: false);
                    break;
                case "debuff":
                case "debuff_def":
                    ProcessDebuffSkill(defender, skill, isATK: false, attackerName: attacker.PlayerName);
                    break;
                case "debuff_atk":
                    ProcessDebuffSkill(defender, skill, isATK: true, attackerName: attacker.PlayerName);
                    break;
                default:
                    ProcessDamageSkill(attacker, defender, skill);
                    break;
            }

            // Set cooldown
            attacker.SkillCooldowns[skillIndex] = Math.Max(1, skill.Cooldown);

            ProcessPostAction();
            return true;
        }

        /// <summary>
        /// Aksi DEFEND — mengurangi damage masuk 50% pada giliran lawan berikutnya.
        /// </summary>
        public bool DoDefend()
        {
            if (CurrentPhase != BattlePhase.WaitingInput) return false;
            var attacker = GetCurrentFighter();
            if (attacker.HasStun) return false;

            CurrentPhase = BattlePhase.ResolvingAction;
            OnPhaseChanged?.Invoke();

            attacker.IsDefending = true;

            OnBattleLog?.Invoke($"🛡 {attacker.PlayerName} mengambil stance pertahanan!");
            OnBattleLog?.Invoke("  Damage masuk pada serangan lawan berikutnya dikurangi 50%.");

            ProcessPostAction();
            return true;
        }

        /// <summary>
        /// Aksi ITEM — gunakan item aktif di battle jika dipilih.
        /// </summary>
        public bool DoItem(int itemIndex)
        {
            if (CurrentPhase != BattlePhase.WaitingInput) return false;
            var user = GetCurrentFighter();
            if (user.HasStun) return false;

            if (itemIndex < 0 || itemIndex >= user.Items.Count)
            {
                OnBattleLog?.Invoke("❌ Item index tidak valid!");
                return false;
            }

            if (user.ItemCooldowns.ContainsKey(itemIndex) && user.ItemCooldowns[itemIndex] > 0)
            {
                OnBattleLog?.Invoke($"❌ {user.Items[itemIndex].Name} masih cooldown ({user.ItemCooldowns[itemIndex]} turn)!");
                return false;
            }

            CurrentPhase = BattlePhase.ResolvingAction;
            OnPhaseChanged?.Invoke();

            Item item = user.Items[itemIndex];
            string effect = item.EffectType.Trim().ToUpperInvariant();
            double val = item.EffectValue;

            OnBattleLog?.Invoke($"🎒 {user.PlayerName} mengaktifkan item: {item.Name}!");

            if (effect.Contains("HEAL") || effect.Contains("HP"))
            {
                int healAmount = item.IsPercentage ? (int)Math.Round(user.MaxHP * (val / 100.0)) : (int)Math.Round(val);
                int actualHeal = Math.Min(healAmount, user.MaxHP - user.CurrentHP);
                user.CurrentHP += actualHeal;
                OnBattleLog?.Invoke($"  💚 Memulihkan +{actualHeal} HP! (HP: {user.CurrentHP}/{user.MaxHP})");
                OnHPChanged?.Invoke(CurrentTurn, user.CurrentHP, user.MaxHP);
            }
            else if (effect.Contains("ATK"))
            {
                int boost = item.IsPercentage ? (int)Math.Round(user.Anomaly.BaseATK * (val / 100.0)) : (int)Math.Round(val);
                user.ATKBuffValue = boost;
                user.ATKBuffTurns = 2;
                OnBattleLog?.Invoke($"  💪 ATK meningkat +{boost} selama 2 turn!");
            }
            else if (effect.Contains("DEF"))
            {
                int boost = item.IsPercentage ? (int)Math.Round(user.Anomaly.BaseDEF * (val / 100.0)) : (int)Math.Round(val);
                user.DEFBuffValue = boost;
                user.DEFBuffTurns = 2;
                OnBattleLog?.Invoke($"  🛡 DEF meningkat +{boost} selama 2 turn!");
            }
            else
            {
                // General boost/heal
                int heal = Math.Max(20, (int)Math.Round(val));
                int actualHeal = Math.Min(heal, user.MaxHP - user.CurrentHP);
                user.CurrentHP += actualHeal;
                OnBattleLog?.Invoke($"  ✨ Efek aktif memulihkan +{actualHeal} HP!");
                OnHPChanged?.Invoke(CurrentTurn, user.CurrentHP, user.MaxHP);
            }

            user.ItemCooldowns[itemIndex] = 2; // 2 turn cooldown
            ProcessPostAction();
            return true;
        }

        // ===== SKILL PROCESSORS =====

        private void ProcessDamageSkill(FighterState attacker, FighterState defender, Skill skill)
        {
            double powerMultiplier = (skill.Power > 0) ? skill.Power : 1.2;
            int damage = CalculateDamage(attacker.GetCurrentATK(), powerMultiplier, defender.GetCurrentDEF(), defender.IsDefending);

            ApplyDamage(defender, damage, GetOpponentNum());
            attacker.TotalDamageDealt += damage;
            defender.IsDefending = false;

            OnBattleLog?.Invoke($"✨ {attacker.PlayerName} menggunakan skill {skill.Name}!");
            OnBattleLog?.Invoke($"  💥 Damage: {damage} (Power: {powerMultiplier:F1}x)");
        }

        private void ProcessHealSkill(FighterState attacker, Skill skill)
        {
            double power = (skill.Power > 0) ? skill.Power : 1.3;
            int healAmount = (int)Math.Round(attacker.Anomaly.BaseHP * (power * 0.15));
            if (healAmount < 25) healAmount = 25;

            int actualHeal = Math.Min(healAmount, attacker.MaxHP - attacker.CurrentHP);
            attacker.CurrentHP += actualHeal;

            OnBattleLog?.Invoke($"💚 {attacker.PlayerName} menggunakan skill {skill.Name}!");
            OnBattleLog?.Invoke($"  Memulihkan +{actualHeal} HP! (HP: {attacker.CurrentHP}/{attacker.MaxHP})");
            OnHPChanged?.Invoke(CurrentTurn, attacker.CurrentHP, attacker.MaxHP);
        }

        private void ProcessPoisonSkill(FighterState attacker, FighterState defender, Skill skill)
        {
            double powerMultiplier = (skill.Power > 0) ? skill.Power * 0.5 : 0.8;
            int initialDamage = CalculateDamage(attacker.GetCurrentATK(), powerMultiplier, defender.GetCurrentDEF(), defender.IsDefending);
            ApplyDamage(defender, initialDamage, GetOpponentNum());
            attacker.TotalDamageDealt += initialDamage;
            defender.IsDefending = false;

            int poisonDmg = Math.Max(15, (int)Math.Round(attacker.GetCurrentATK() * 0.2));
            defender.HasPoison = true;
            defender.PoisonDamage = poisonDmg;
            defender.PoisonTurns = 3;

            OnBattleLog?.Invoke($"☠ {attacker.PlayerName} menggunakan {skill.Name}!");
            OnBattleLog?.Invoke($"  Damage: {initialDamage}");
            OnBattleLog?.Invoke($"  {defender.PlayerName} terkena POISON! (-{poisonDmg} HP/turn, 3 turn)");
        }

        private void ProcessStunSkill(FighterState attacker, FighterState defender, Skill skill)
        {
            double powerMultiplier = (skill.Power > 0) ? skill.Power * 0.6 : 0.7;
            int damage = CalculateDamage(attacker.GetCurrentATK(), powerMultiplier, defender.GetCurrentDEF(), defender.IsDefending);
            ApplyDamage(defender, damage, GetOpponentNum());
            attacker.TotalDamageDealt += damage;
            defender.IsDefending = false;

            if (!defender.WasStunnedLastTurn)
            {
                defender.HasStun = true;
                OnBattleLog?.Invoke($"⚡ {attacker.PlayerName} menggunakan {skill.Name}!");
                OnBattleLog?.Invoke($"  Damage: {damage}");
                OnBattleLog?.Invoke($"  {defender.PlayerName} terkena STUN! (Giliran berikutnya dilewati)");
            }
            else
            {
                OnBattleLog?.Invoke($"⚡ {attacker.PlayerName} menggunakan {skill.Name}!");
                OnBattleLog?.Invoke($"  Damage: {damage}");
                OnBattleLog?.Invoke($"  {defender.PlayerName} kebal stun (anti-stun berturut-turut).");
            }
        }

        private void ProcessShieldSkill(FighterState attacker, Skill skill)
        {
            double power = (skill.Power > 0) ? skill.Power : 1.2;
            int shieldAmount = (int)Math.Round(attacker.Anomaly.BaseHP * (power * 0.2));
            attacker.ShieldHP = shieldAmount;

            OnBattleLog?.Invoke($"🔰 {attacker.PlayerName} menggunakan {skill.Name}!");
            OnBattleLog?.Invoke($"  Mendapatkan Shield: {shieldAmount} HP");
        }

        private void ProcessBuffSkill(FighterState fighter, Skill skill, bool isATK)
        {
            double power = (skill.Power > 0) ? skill.Power : 1.3;
            int baseVal = isATK ? fighter.Anomaly.BaseATK : fighter.Anomaly.BaseDEF;
            int buffVal = Math.Max(10, (int)Math.Round(baseVal * (power - 1.0 + 0.2)));

            if (isATK)
            {
                fighter.ATKBuffValue = buffVal;
                fighter.ATKBuffTurns = 3;
            }
            else
            {
                fighter.DEFBuffValue = buffVal;
                fighter.DEFBuffTurns = 3;
            }

            string statName = isATK ? "ATK" : "DEF";
            OnBattleLog?.Invoke($"💪 {fighter.PlayerName} menggunakan {skill.Name}!");
            OnBattleLog?.Invoke($"  {statName} +{buffVal} selama 3 turn");
        }

        private void ProcessDebuffSkill(FighterState target, Skill skill, bool isATK, string attackerName)
        {
            double power = (skill.Power > 0) ? skill.Power : 1.2;
            int baseVal = isATK ? target.Anomaly.BaseATK : target.Anomaly.BaseDEF;
            int debuffVal = Math.Max(10, (int)Math.Round(baseVal * 0.25));

            if (isATK)
            {
                target.ATKBuffValue = -debuffVal;
                target.ATKBuffTurns = 3;
            }
            else
            {
                target.DEFBuffValue = -debuffVal;
                target.DEFBuffTurns = 3;
            }

            string statName = isATK ? "ATK" : "DEF";
            OnBattleLog?.Invoke($"🔻 {attackerName} menggunakan {skill.Name}!");
            OnBattleLog?.Invoke($"  {target.PlayerName} {statName} -{debuffVal} selama 3 turn");
        }

        // ===== DAMAGE CALCULATION =====

        private int CalculateDamage(int atk, double powerSkill, int def, bool isDefending)
        {
            double rawDamage = (atk * powerSkill) - (def * 0.5);
            int damage = Math.Max(1, (int)Math.Round(rawDamage));

            if (isDefending)
            {
                damage = Math.Max(1, damage / 2);
            }

            return damage;
        }

        private void ApplyDamage(FighterState target, int damage, int targetPlayerNum)
        {
            if (target.ShieldHP > 0)
            {
                if (damage <= target.ShieldHP)
                {
                    target.ShieldHP -= damage;
                    OnBattleLog?.Invoke($"  🔰 Shield menyerap {damage} damage! (Sisa shield: {target.ShieldHP})");
                    OnHPChanged?.Invoke(targetPlayerNum, target.CurrentHP, target.MaxHP);
                    return;
                }
                else
                {
                    int absorbed = target.ShieldHP;
                    damage -= target.ShieldHP;
                    target.ShieldHP = 0;
                    OnBattleLog?.Invoke($"  🔰 Shield menyerap {absorbed} damage! Shield pecah!");
                }
            }

            target.CurrentHP -= damage;
            if (target.CurrentHP < 0) target.CurrentHP = 0;
            OnHPChanged?.Invoke(targetPlayerNum, target.CurrentHP, target.MaxHP);
        }

        // ===== STATE MACHINE =====

        private void ProcessTurnStart()
        {
            var fighter = GetCurrentFighter();

            OnBattleLog?.Invoke($"--- Turn {TurnNumber}: Giliran {fighter.PlayerName} ---");

            // 1. Passive Regen per turn (jika ada item Regen)
            if (fighter.RegenPerTurn > 0 && fighter.CurrentHP < fighter.MaxHP && fighter.CurrentHP > 0)
            {
                int heal = Math.Min(fighter.RegenPerTurn, fighter.MaxHP - fighter.CurrentHP);
                fighter.CurrentHP += heal;
                OnBattleLog?.Invoke($"  💚 Regen pasif memulihkan +{heal} HP ({fighter.CurrentHP}/{fighter.MaxHP})");
                OnHPChanged?.Invoke(CurrentTurn, fighter.CurrentHP, fighter.MaxHP);
            }

            // 2. Poison Damage di awal giliran
            if (fighter.HasPoison && fighter.PoisonTurns > 0)
            {
                fighter.CurrentHP -= fighter.PoisonDamage;
                if (fighter.CurrentHP < 0) fighter.CurrentHP = 0;
                fighter.PoisonTurns--;

                OnBattleLog?.Invoke($"  ☠ {fighter.PlayerName} menderita efek poison! -{fighter.PoisonDamage} HP");
                OnHPChanged?.Invoke(CurrentTurn, fighter.CurrentHP, fighter.MaxHP);

                if (fighter.PoisonTurns <= 0)
                {
                    fighter.HasPoison = false;
                    OnBattleLog?.Invoke($"  Efek poison pada {fighter.PlayerName} telah hilang.");
                }

                if (fighter.CurrentHP <= 0)
                {
                    CurrentPhase = BattlePhase.CheckWinCondition;
                    CheckAndEndBattle();
                    return;
                }
            }

            // 3. Stun Check
            if (fighter.HasStun)
            {
                fighter.HasStun = false;
                fighter.WasStunnedLastTurn = true;

                OnBattleLog?.Invoke($"  ⚡ {fighter.PlayerName} dalam kondisi STUN! Giliran dilewati.");
                OnStunSkipTurn?.Invoke(CurrentTurn);

                ProcessTurnEnd();
                return;
            }
            else
            {
                fighter.WasStunnedLastTurn = false;
            }

            CurrentPhase = BattlePhase.WaitingInput;
            OnPhaseChanged?.Invoke();
            OnTurnChanged?.Invoke(CurrentTurn);
        }

        private void ProcessPostAction()
        {
            CurrentPhase = BattlePhase.CheckWinCondition;
            OnPhaseChanged?.Invoke();

            if (CheckAndEndBattle()) return;

            ProcessTurnEnd();
        }

        private void ProcessTurnEnd()
        {
            CurrentPhase = BattlePhase.TurnEnd;
            OnPhaseChanged?.Invoke();

            var fighter = GetCurrentFighter();

            // Kurangi cooldown skill
            List<int> skillKeys = new List<int>(fighter.SkillCooldowns.Keys);
            foreach (int key in skillKeys)
            {
                if (fighter.SkillCooldowns[key] > 0)
                    fighter.SkillCooldowns[key]--;
            }

            // Kurangi cooldown item
            List<int> itemKeys = new List<int>(fighter.ItemCooldowns.Keys);
            foreach (int key in itemKeys)
            {
                if (fighter.ItemCooldowns[key] > 0)
                    fighter.ItemCooldowns[key]--;
            }

            // Kurangi durasi buff/debuff
            if (fighter.ATKBuffTurns > 0)
            {
                fighter.ATKBuffTurns--;
                if (fighter.ATKBuffTurns <= 0) fighter.ATKBuffValue = 0;
            }
            if (fighter.DEFBuffTurns > 0)
            {
                fighter.DEFBuffTurns--;
                if (fighter.DEFBuffTurns <= 0) fighter.DEFBuffValue = 0;
            }

            // Pindah giliran
            CurrentTurn = (CurrentTurn == 1) ? 2 : 1;
            TurnNumber++;

            ProcessTurnStart();
        }

        private bool CheckAndEndBattle()
        {
            int winner = 0;
            if (Fighter1.CurrentHP <= 0) winner = 2;
            else if (Fighter2.CurrentHP <= 0) winner = 1;

            if (winner == 0) return false;

            CurrentPhase = BattlePhase.BattleOver;
            OnPhaseChanged?.Invoke();

            var winFighter = (winner == 1) ? Fighter1 : Fighter2;

            string mostUsedSkill1 = GetMostUsedSkill(Fighter1);
            string mostUsedSkill2 = GetMostUsedSkill(Fighter2);

            string summary = $"🏆 {winFighter.PlayerName} MENANG!\n\n" +
                            $"--- Ringkasan Battle ---\n" +
                            $"Total Giliran: {TurnNumber}\n\n" +
                            $"{Fighter1.PlayerName} ({Fighter1.Anomaly.Name}):\n" +
                            $"  Total Damage: {Fighter1.TotalDamageDealt}\n" +
                            $"  Skill Andalan: {mostUsedSkill1}\n" +
                            $"  Sisa HP: {Fighter1.CurrentHP}/{Fighter1.MaxHP}\n\n" +
                            $"{Fighter2.PlayerName} ({Fighter2.Anomaly.Name}):\n" +
                            $"  Total Damage: {Fighter2.TotalDamageDealt}\n" +
                            $"  Skill Andalan: {mostUsedSkill2}\n" +
                            $"  Sisa HP: {Fighter2.CurrentHP}/{Fighter2.MaxHP}";

            OnBattleLog?.Invoke("");
            OnBattleLog?.Invoke("=== BATTLE END ===");
            OnBattleLog?.Invoke(summary);

            OnBattleEnd?.Invoke(winner, summary);
            return true;
        }

        private string GetMostUsedSkill(FighterState fighter)
        {
            if (fighter.SkillUsageCount.Count == 0) return "(Attack Biasa)";
            return fighter.SkillUsageCount.OrderByDescending(kv => kv.Value).First().Key;
        }

        // ===== PUBLIC HELPERS =====

        public bool IsSkillOnCooldown(int playerNum, int skillIndex)
        {
            var fighter = (playerNum == 1) ? Fighter1 : Fighter2;
            if (skillIndex < 0 || skillIndex >= fighter.Skills.Count) return false;
            return fighter.SkillCooldowns.ContainsKey(skillIndex) && fighter.SkillCooldowns[skillIndex] > 0;
        }

        public int GetSkillCooldownRemaining(int playerNum, int skillIndex)
        {
            var fighter = (playerNum == 1) ? Fighter1 : Fighter2;
            if (fighter.SkillCooldowns.ContainsKey(skillIndex))
                return fighter.SkillCooldowns[skillIndex];
            return 0;
        }

        public bool IsItemOnCooldown(int playerNum, int itemIndex)
        {
            var fighter = (playerNum == 1) ? Fighter1 : Fighter2;
            if (itemIndex < 0 || itemIndex >= fighter.Items.Count) return false;
            return fighter.ItemCooldowns.ContainsKey(itemIndex) && fighter.ItemCooldowns[itemIndex] > 0;
        }

        public int GetItemCooldownRemaining(int playerNum, int itemIndex)
        {
            var fighter = (playerNum == 1) ? Fighter1 : Fighter2;
            if (fighter.ItemCooldowns.ContainsKey(itemIndex))
                return fighter.ItemCooldowns[itemIndex];
            return 0;
        }

        public int GetHpPercentage(int playerNum)
        {
            var fighter = (playerNum == 1) ? Fighter1 : Fighter2;
            if (fighter.MaxHP <= 0) return 0;
            return (int)Math.Round((double)fighter.CurrentHP * 100 / fighter.MaxHP);
        }
    }
}
