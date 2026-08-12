# 🎮 SESI 4: Testing Guide - Gameplay Full hingga Win/Lose
## Anomaly Versus Battle System Testing

---

## 📋 **SETUP SEBELUM TESTING**

### **1. Pastikan Database Siap**
```
✓ Database: anomaly_versus_db
✓ Tables: Player, Anomaly, Skill, Item, MatchHistory, MatchItemUsed
✓ Dummy data sudah di-seed
✓ Koneksi test: Form1_Load sudah test koneksi
```

**Cara cek database:**
- Buka phpMyAdmin → Select database `anomaly_versus_db`
- Cek table `Anomaly` → harus ada 6+ anomaly (Ignis, Aqua, Terra, etc)
- Cek table `Skill` → harus ada 6+ skill dengan AnomalyID
- Cek table `Item` → harus ada 8+ item dengan EffectType

### **2. Pastikan Folder Assets Benar**
```
WinFormsApp1/WinFormsApp1/
└── Assets/
	└── Images/
		├── placeholder.png
		└── Anomaly/
			├── Ignis.png (atau nama anomaly lain)
			└── [anomaly images]
```

**Jika belum ada gambar Anomaly:**
- Gunakan placeholder.png sebagai fallback
- Tambahkan gambar Anomaly nanti (naming: {Anomaly.Name}.png)

### **3. Build & Run Aplikasi**
```bash
cd WinFormsApp1
dotnet build -c Debug
dotnet run
```

---

## 🎮 **SKENARIO TESTING #1: GAMEPLAY PENUH (Normal Flow)**

### **Tahap 1: Main Menu**
```
Expected UI:
┌────────────────────────────┐
│   ANOMALY VERSUS           │
│   [Mulai Game]             │
│   [Keluar]                 │
└────────────────────────────┘

Actions:
1. Klik "Mulai Game"
✓ Should navigate → PlayerNameForm
```

### **Tahap 2: Input Nama Pemain**
```
Expected UI:
┌────────────────────────────┐
│ Masukkan Nama Pemain       │
│ Player 1: [textbox]        │
│ Player 2: [textbox]        │
│ [Lanjut] [Batal]           │
└────────────────────────────┘

Actions:
1. Input Player 1 name: "Budi"
2. Input Player 2 name: "Andi"
3. Klik "Lanjut"

Validasi:
✓ Nama tidak boleh kosong
✓ Nama tidak boleh sama
✓ Database insert/update player record
✓ GameSession.Player1Name = "Budi"
✓ GameSession.Player2Name = "Andi"
✓ Should navigate → AnomalySelectionForm
```

### **Tahap 3: Pilih Anomaly**
```
Expected UI:
┌──────────────────────────────────┐
│ Pilih Anomaly Untuk Tiap Pemain  │
│ [ListBox Anomaly]                │
│ ┌─────────────────────────────┐  │
│ │ Ignis (Attacker) - HP:100   │  │
│ │ Aqua (Defender) - HP:120    │  │
│ │ Terra (Support) - HP:90     │  │
│ │ ...                         │  │
│ └─────────────────────────────┘  │
│                                  │
│ Player 1: (belum dipilih)        │
│ Player 2: (belum dipilih)        │
│                                  │
│ [Pilih P1] [Pilih P2] [Lanjut]   │
└──────────────────────────────────┘

Actions:
1. Click "Ignis" di ListBox
2. Klik "Pilih Player 1"
   ✓ Should show: "Player 1 (Budi): Ignis"
   ✓ GameSession.Player1Anomaly = Ignis object
3. Click "Aqua" di ListBox
4. Klik "Pilih Player 2"
   ✓ Should show: "Player 2 (Andi): Aqua"
   ✓ GameSession.Player2Anomaly = Aqua object
5. Klik "Lanjut"
   ✓ Validasi: kedua player harus pilih
   ✓ Should navigate → ItemSelectionForm
```

### **Tahap 4: Pilih Item Support**
```
Expected UI:
┌──────────────────────────────────┐
│ Pilih 2 Item Support per Player  │
│ [CheckedListBox Item]            │
│ ☐ Heal Potion (heal: 20)        │
│ ☐ Attack Boost (attack_boost: 5) │
│ ☐ Defense Boost (defense_boost: 3)│
│ ...                              │
│                                  │
│ [Pilih P1] [Mulai Battle]        │
│ Player 1: 0 item dipilih         │
│ Player 2: 0 item dipilih         │
└──────────────────────────────────┘

Actions:
1. Check 2 item untuk Player 1 (misalnya: "Heal Potion" & "Atk Boost")
2. Klik "Pilih Untuk Player 1"
   ✓ Harus check tepat 2 item (validasi)
   ✓ Should show: "Player 1: 2 item dipilih"
   ✓ Switch ke input Player 2
   ✓ Tombol berubah ke "Pilih Untuk Player 2"
3. Check 2 item untuk Player 2 (berbeda dari P1 boleh/sama boleh)
4. Klik "Pilih Untuk Player 2"
   ✓ Harus check tepat 2 item
   ✓ Should show: "Player 2: 2 item dipilih"
   ✓ Tombol "Mulai Battle" enabled
5. Klik "Mulai Battle"
   ✓ Show confirm dialog dengan summary
   ✓ Klik "Yes"
   ✓ Should navigate → FormBattle
```

### **Tahap 5: Battle Dimulai**
```
Expected UI:
┌────────────────────────────────────────────────────────────────┐
│           ANOMALY VERSUS — BATTLE                              │
├──────────────────┬──────────────────┬──────────────────────────┤
│   [P1 Panel]     │  [LOG PANEL]     │     [P2 Panel]           │
│                  │                  │                          │
│  [Picture]       │ ⏳ Giliran: Budi  │  [Picture]               │
│  Budi            │                  │  Andi                    │
│  Ignis           │ Battle log:      │  Aqua                    │
│  HP: 100/100     │ • Koneksi ✓      │  HP: 120/120             │
│  [Bar█████░░]  │ • P1 chose Ignis │  [Bar█████░░]            │
│                  │ • P2 chose Aqua  │                          │
│                  │ • Battle start!  │                          │
│                  │                  │                          │
├────────────────────────────────────────────────────────────────┤
│  [Attack] [Skill ▼] [Defend] [Item ▼]                          │
└────────────────────────────────────────────────────────────────┘

Initial Checks:
✓ Pictures loaded (Ignis & Aqua) atau placeholder jika tidak ada
✓ HP bars full (100% green)
✓ Battle log menampilkan persiapan
✓ Giliran: Budi (Player 1 mulai duluan)
✓ Skill dropdown populated dengan Ignis skills
✓ Item dropdown populated dengan 2 item Player 1
```

### **Tahap 6: Battle Action - Player 1 Attack**
```
Actions:
1. Klik tombol [Attack]
   ✓ Calculate: Ignis.ATK - Aqua.DEF = damage (min 1)
   ✓ Aqua.HP -= damage
   ✓ Log: "Budi menyerang dengan serangan biasa! Damage: 15"
   ✓ HP bar P2 update & berubah warna
   ✓ Giliran pindah ke Player 2 (Andi)

Expected Result:
│  Aqua HP update dari 120 → 105 (misal damage 15)
│  Bar color: still green (105/120 = 87%)
│  Log add: "Budi menyerang dengan serangan biasa! Damage: 15"
│  Turn indicator: "⏳ Giliran: Andi"
│  Skill dropdown update: sekarang menampilkan Aqua skills
│  Item dropdown update: sekarang menampilkan Aqua items
```

### **Tahap 7: Battle Action - Player 2 Skill**
```
Actions:
1. Select skill dari dropdown (misal: Aqua punya skill "Water Blast" damage 25)
2. Klik [Pakai Skill]
   ✓ Check cooldown (awal battle semua skill ready)
   ✓ Apply damage: Ignis.HP -= 25
   ✓ Set cooldown untuk skill ini = Skill.MpCost
   ✓ Log: "Andi menggunakan skill Water Blast! Damage: 25. Cooldown: 2 turn"
   ✓ HP bar P1 update
   ✓ Giliran pindah ke Player 1

Expected Result:
│  Ignis HP update dari 100 → 75 (misal damage 25)
│  Bar color: still green (75/100 = 75%)
│  Log add: "Andi menggunakan skill Water Blast! Damage: 25. Cooldown: 2 turn"
│  Skill dropdown P2: "Water Blast [CD: 2 turn]" (strikeout/disabled?)
│  Turn indicator: "⏳ Giliran: Budi"
```

### **Tahap 8: Battle Action - Player 1 Defend**
```
Actions:
1. Klik [Defend]
   ✓ Set IsDefendingP1 = true
   ✓ Next turn damage akan -50%
   ✓ Log: "Budi mengambil stance pertahanan! Incoming damage turn ini akan dikurangi 50%."
   ✓ Giliran pindah ke Player 2

Expected Result:
│  Log add: "Budi mengambil stance pertahanan!..."
│  Next turn: P2 attack akan -50% damage
│  Turn indicator: "⏳ Giliran: Andi"
```

### **Tahap 9: Battle Action - Player 2 Attack (dengan Defend aktif P1)**
```
Actions:
1. Player 2 Attack
   ✓ Calculate: Aqua.ATK (25) - Ignis.DEF (10) = 15
   ✓ BUT P1 is defending: 15 / 2 = 7 (pembulatan ke bawah)
   ✓ Ignis.HP -= 7 (bukan 15)
   ✓ Log: "Andi menyerang dengan serangan biasa! Damage: 7 (dikurangi 50% karena lawan defend)"

Expected Result:
│  Ignis HP: 75 - 7 = 68
│  Log: "...Damage: 7 (dikurangi 50%...)"
│  Defend status hilang untuk P1 (hanya berlaku 1 turn)
│  Turn: kembali ke P1
```

### **Tahap 10: Battle Action - Player 1 Item (Heal)**
```
Actions:
1. Select item "Heal Potion" dari dropdown
2. Klik [Pakai Item]
   ✓ Item.EffectType = "heal"
   ✓ Ignis.HP += 20 (capped di MaxHp)
   ✓ Set cooldown item = 2 turn
   ✓ Log: "Budi menggunakan item Heal Potion! Heal: +20 HP"
   ✓ Giliran pindah ke Player 2

Expected Result:
│  Ignis HP: 68 + 20 = 88
│  Log: "Budi menggunakan item Heal Potion! Heal: +20 HP"
│  Item dropdown: "Heal Potion [CD: 2 turn]" (disabled)
│  Turn: P2
│  HP bar P1: 88/100 = 88% (update)
```

### **Tahap 11: Battle Lanjut sampai Salah Satu HP ≤ 0**
```
Continue actions (Attack/Skill/Defend/Item) sampai:
✓ Salah satu pemain HP → 0 atau negatif
✓ CheckWinner() return 1 (P1 menang) atau 2 (P2 menang)
✓ Semua tombol aksi disable
✓ Battle log show: "=== BATTLE END ==="
✓ Show MessageBox hasil kemenangan
```

---

## 🏆 **Tahap 12: Battle End & Winner Announcement**

### **Expected Flow:**
```
1. Battle akhir:
   ├─ Aqua HP: 5/120
   ├─ Player 2 Attack Ignis
   ├─ Ignis HP: 30 - (25-10) = 15
   └─ Player 1 Attack Aqua
	  ├─ Damage: 20-5 = 15
	  ├─ Aqua HP: 5 - 15 = -10 (≤ 0, Aqua DEAD)
	  ├─ CheckWinner() = 1 (P1 win)
	  └─ Disable ALL buttons

2. Battle Log show:
   │ Andi menyerang ...
   │ Ignis HP update
   │ Budi menyerang ...
   │ Damage: 15
   │ 
   │ === BATTLE END ===
   │ Budi menyerang dengan serangan biasa!
   │ Damage: 15 (mengalahkan Aqua)

3. MessageBox:
   ┌─────────────────────────────────┐
   │ 🏆 Budi MENANG!               │
   │                                │
   │ Ignis mengalahkan Aqua         │
   │                                │
   │ [OK]                           │
   └─────────────────────────────────┘

4. Database Update (setelah OK):
   ✓ MatchHistory INSERT:
	 - WinnerPlayerID: Budi's ID
	 - LoserPlayerID: Andi's ID
	 - WinnerAnomalyName: "Ignis"
	 - LoserAnomalyName: "Aqua"
	 - PlayedAt: NOW()

   ✓ Player UPDATE (Budi):
	 - TotalWins++

   ✓ Player UPDATE (Andi):
	 - TotalLosses++

5. Navigate:
   ✓ Battle log show: "✓ Match history saved to database"
   ✓ Close FormBattle
   ✓ GameSession.ResetSession()
   ✓ Return to MainMenuForm
```

---

## ✅ **VERIFICATION CHECKLIST**

### **Gameplay Mechanics**
- [ ] Damage calculation benar (ATK - DEF)
- [ ] Defend reduce damage 50%
- [ ] Skill cooldown display & blocking fungsional
- [ ] Item cooldown 2 turn bekerja
- [ ] Heal item restore HP correct
- [ ] Boost items apply effect 2 turn
- [ ] Turn bergantian correct
- [ ] HP bar color change (green→yellow→red)
- [ ] Winner detected saat HP ≤ 0
- [ ] All buttons disabled saat battle end

### **UI/UX**
- [ ] Images load (Anomaly pictures)
- [ ] HPbar update real-time
- [ ] Battle log auto-scroll
- [ ] Battle log readable & clear
- [ ] Skill dropdown show cooldown info
- [ ] Item dropdown show cooldown info
- [ ] Turn indicator jelas siapa giliran
- [ ] No crashes atau error dialogs

### **Database**
- [ ] MatchHistory record created
- [ ] Winner ID correct
- [ ] Loser ID correct
- [ ] Anomaly names saved correct
- [ ] Timestamp recorded
- [ ] Player wins stat incremented
- [ ] Player losses stat incremented
- [ ] phpMyAdmin menunjukkan entry baru

### **Navigation**
- [ ] MainMenu → PlayerName ✓
- [ ] PlayerName → AnomalySelect ✓
- [ ] AnomalySelect → ItemSelect ✓
- [ ] ItemSelect → Battle ✓
- [ ] Battle End → MainMenu ✓
- [ ] Bisa main multiple rounds

---

## 🐛 **TROUBLESHOOTING**

### **Database Connection Failed**
```
Error: "Koneksi gagal"
Solution: 
1. Buka phpMyAdmin
2. Cek MySQL running
3. Verify connection string di App.config
4. Cek port 3306 accessible
```

### **Image Not Load**
```
Shows: placeholder.png atau kosong
Solution:
1. Check Assets/Images/Anomaly/ folder exists
2. Verify file names match Anomaly.Name (case-sensitive)
3. Use placeholder.png first untuk testing
```

### **Battle Logic Error**
```
Example: Damage calculation wrong / Cooldown not working
Solution:
1. Check BattleManager.cs logic
2. Add Debug.WriteLine() untuk trace
3. Check calculations di BattleManager methods
4. Verify GameSession data sebelum battle start
```

### **HP Bar Not Updating**
```
Solution:
1. Check RefreshUI() dipanggil setelah setiap action
2. Check pbHpP1.Value assignment
3. Verify UpdateHpBarColor() logic
```

---

## 📊 **EXPECTED DATABASE RESULT**

### After Satu Battle (Budi win vs Andi):

**Table: Player**
```
PlayerID | PlayerName | TotalWins | TotalLosses | TotalMatches
1        | Budi       | 1         | 0           | 1
2        | Andi       | 0         | 1           | 1
```

**Table: MatchHistory**
```
MatchID | WinnerPlayerID | LoserPlayerID | WinnerAnomalyName | LoserAnomalyName | PlayedAt
1       | 1              | 2             | Ignis             | Aqua             | 2024-01-15 10:30:45
```

---

## 🎬 **Multiple Battles Test**

Setelah satu complete battle, test:
1. Play another round (dari Main Menu)
2. Different player names
3. Different anomalies & items
4. Verify DB records untuk multiple matches
5. Verify player stats accumulate (win count bertambah)

---

## ✨ **SUCCESS CRITERIA**

Sesi 4 dianggap **SUKSES** jika:
- ✅ Full gameplay flow berjalan tanpa crash
- ✅ Battle logic bekerja sesuai spec
- ✅ Winner correctly detected & announced
- ✅ Database records match expected values
- ✅ Player bisa play multiple rounds
- ✅ All UI elements update correctly
- ✅ Zero compilation warnings/errors

**Tim siap untuk Sesi 5 (Fine Tuning & Polish)!** 🚀

