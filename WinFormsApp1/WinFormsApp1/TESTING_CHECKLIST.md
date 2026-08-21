# ⚡ QUICK TESTING CHECKLIST - Sesi 4
## Run game dari MainMenu → Battle → Win/Lose

---

## 🚀 START TESTING

### **Step 1: Launch Application**
```bash
cd WinFormsApp1
dotnet run
```

Wait for MainMenuForm to appear

---

### **Step 2: Main Menu → Player Names**

```
UI Check:
✓ Title: "ANOMALY VERSUS"
✓ Button: "Mulai Game"
✓ Button: "Keluar"

Action: Click "Mulai Game"
Expected: PlayerNameForm appears
```

---

### **Step 3: Input Player Names**

```
UI Check:
✓ Input label: "Player 1:"
✓ Input label: "Player 2:"
✓ Button: "Lanjut"
✓ Button: "Batal"

Action Test:
1. Click Lanjut tanpa input
   → Should show error "Nama tidak boleh kosong"
2. Input P1="TestP1", P2="TestP1" (same)
   → Should show error "Nama tidak boleh sama"
3. Input P1="TestP1", P2="TestP2"
   → Click Lanjut
   Expected: AnomalySelectionForm appears
```

---

### **Step 4: Select Anomalies**

```
UI Check:
✓ ListBox showing anomalies
✓ Format: "Name (Role) - HP:X ATK:X DEF:X"
✓ Button: "Pilih Player 1"
✓ Button: "Pilih Player 2"
✓ Button: "Lanjut"

Action Test:
1. Click listbox item (e.g., "Ignis (Attacker) - HP:100...")
2. Click "Pilih Player 1"
   → Should show green text: "Player 1 (TestP1): Ignis"
3. Click different anomaly (e.g., "Aqua (Defender)...")
4. Click "Pilih Player 2"
   → Should show green text: "Player 2 (TestP2): Aqua"
5. Click "Lanjut"
   Expected: ItemSelectionForm appears
```

---

### **Step 5: Select Items**

```
UI Check:
✓ CheckedListBox showing items
✓ Format: "Name (EffectType: Value)"
✓ Label: "Pilih 2 item untuk Player 1..."
✓ Button: "Pilih Untuk Player 1"
✓ Button: "Mulai Battle"

Action Test - Player 1 Items:
1. Uncheck semua, check 1 item only
2. Click "Pilih Untuk Player 1"
   → Should show error: "Harus memilih tepat 2 item!"
3. Check exactly 2 items
4. Click "Pilih Untuk Player 1"
   → Should show: "Player 1: 2 item dipilih"
   → Form switches untuk Player 2 input
   → Instruction text berubah jadi "Pilih 2 item untuk Player 2..."
   → Button berubah jadi "Pilih Untuk Player 2"

Action Test - Player 2 Items:
1. Check exactly 2 items (boleh berbeda dengan P1)
2. Click "Pilih Untuk Player 2"
   → Should show: "Player 2: 2 item dipilih"
   → Button "Mulai Battle" should be ENABLED (bukan gray)
3. Click "Mulai Battle"
   → Should show MessageBox dengan summary
   → Klik "Yes"
   Expected: FormBattle appears
```

---

## 🎮 BATTLE PHASE TESTING

### **Initial Battle State**

```
UI Elements Check:
✓ Left Panel: Player 1 info
  - Picture (Ignis) atau placeholder
  - Name: "TestP1"
  - Anomaly: "Ignis"
  - HP Bar (full green)
  - HP Text: "HP: 100 / 100"

✓ Center Panel: Battle Log
  - Label: "⏳ Giliran: TestP1 (Player 1)"
  - ListBox menampilkan battle history:
	• "=== ANOMALY VERSUS BATTLE START ==="
	• "TestP1 menggunakan Ignis"
	• "TestP2 menggunakan Aqua"

✓ Right Panel: Player 2 info
  - Picture (Aqua) atau placeholder
  - Name: "TestP2"
  - Anomaly: "Aqua"
  - HP Bar (full green)
  - HP Text: "HP: 120 / 120"

✓ Action Panel (Bottom):
  - Button: "⚔ Attack"
  - ComboBox + Button: "✨ Pakai Skill"
  - Button: "🛡 Defend"
  - ComboBox + Button: "🎒 Pakai Item"
  - All buttons enabled (Player 1 turn)
```

---

### **TEST #1: Attack Action**

```
Action: Player 1 clicks [Attack]

Expected Results:
✓ Damage calculated: Ignis.ATK (25) - Aqua.DEF (15) = 10
✓ Aqua.HP: 120 - 10 = 110
✓ Log updated: "TestP1 menyerang dengan serangan biasa!"
✓ Log show: "Damage: 10"
✓ HP Bar P2 update (110/120)
✓ Color: still green (91%)
✓ Turn indicator change to: "⏳ Giliran: TestP2 (Player 2)"
✓ Skill dropdown switch to Aqua skills
✓ Item dropdown switch to Aqua items

Next Player (P2) should be able to act
```

---

### **TEST #2: Skill Action (with Cooldown)**

```
Setup: Player 2 turn
Action: Select skill dari dropdown, click [Pakai Skill]

Example: Select "Water Blast" (damage 25, cooldown 2)

Expected Results:
✓ Damage applied: Ignis.HP: 100 - 25 = 75
✓ Log show: "TestP2 menggunakan skill Water Blast!"
✓ Log show: "Damage: 25"
✓ Log show: "Cooldown: 2 turn"
✓ Skill dropdown updated: "Water Blast [CD: 2 turn]" (should appear grayed)
✓ HP Bar P1 update (75/100)
✓ Color: still green (75%)
✓ Turn change to Player 1

Next turns:
- Turn 3 (P1 turn): Aqua skill still [CD: 2 turn]
- Turn 4 (P2 turn): Aqua skill becomes [CD: 1 turn]
- Turn 5 (P2 turn): Aqua skill becomes ready (CD removed)
```

---

### **TEST #3: Defend Action**

```
Setup: Player 1 turn
Action: Click [Defend]

Expected Results:
✓ Log show: "TestP1 mengambil stance pertahanan!"
✓ Log show: "Incoming damage turn ini akan dikurangi 50%."
✓ Turn indicator change to TestP2
✓ IsDefendingP1 = true internally

Next Action (P2 attacks):
- Normal damage: P2.ATK - P1.DEF = X
- With Defend: X / 2 (floor) = actual damage
- Log should show: "Damage: Y (dikurangi 50% karena lawan defend)"
```

---

### **TEST #4: Item Action (Heal)**

```
Setup: Player 1 turn, P1.HP = 75
Action: Select "Heal Potion" (heal 20), click [Pakai Item]

Expected Results:
✓ Heal applied: 75 + 20 = 95 (capped at max=100)
✓ Log show: "TestP1 menggunakan item Heal Potion!"
✓ Log show: "Heal: +20 HP"
✓ Item dropdown: "Heal Potion [CD: 2 turn]"
✓ HP Bar P1 update (95/100)
✓ Turn change to Player 2

Note: Item cooldown same as skill cooldown (2 turn)
```

---

### **TEST #5: HP Bar Color Change**

```
Test progression:
1. HP > 50%: Color should be GREEN
   Example: 75/100 = 75% → green ✓

2. HP 25-50%: Color should be YELLOW
   Example: 40/100 = 40% → yellow ✓

3. HP < 25%: Color should be RED
   Example: 20/100 = 20% → red ✓

Action: Continue battle until someone drops to low HP
Monitor HP bar color changes
```

---

## 🏆 BATTLE END TEST

### **TEST #6: Win Condition**

```
Setup: Continue battle until someone HP ≤ 0

Example:
- Current: P1 HP = 5/100, P2 HP = 30/120
- P1 turn: Attack
- Damage: 20 - 10 = 10
- P2 HP: 30 - 10 = 20
- P2 turn: Attack
- Damage: 25 - 15 = 10
- P1 HP: 5 - 10 = -5 (≤ 0, DEAD!)

Expected Results:
✓ CheckWinner() returns 2 (P2 wins)
✓ All buttons immediately disabled
✓ Battle log show:
  │ TestP2 menyerang dengan serangan biasa!
  │ Damage: 10
  │ 
  │ === BATTLE END ===
  │ TestP2 menyerang dengan serangan biasa!
  │ Damage: 10 (mengalahkan Ignis)

✓ MessageBox appears:
  ┌─────────────────────────┐
  │ 🏆 TestP2 MENANG!      │
  │ Aqua mengalahkan Ignis  │
  │         [OK]            │
  └─────────────────────────┘

✓ Log update: "✓ Match history saved to database"
```

---

## 📦 DATABASE VERIFICATION

### After Battle Ends & DB Save

**Check phpMyAdmin:**

1. Go to table: `Player`
   ```
   TestP1: TotalWins = 0, TotalLosses++
   TestP2: TotalWins++, TotalLosses = 0
   ```

2. Go to table: `MatchHistory`
   ```
   New row added:
   WinnerPlayerID = TestP2's ID
   LoserPlayerID = TestP1's ID
   WinnerAnomalyName = "Aqua"
   LoserAnomalyName = "Ignis"
   PlayedAt = current datetime
   ```

**If DB save successful:**
✓ Battle log show checkmark: "✓ Match history saved"
✓ No error message
```

---

## 🔄 RETURN TO MENU

```
Expected Flow:
✓ MessageBox [OK] clicked
✓ FormBattle closes
✓ GameSession.ResetSession() called
✓ Return to MainMenuForm
✓ Can play another round

Test Multiple Rounds:
1. Play battle #1: P1 wins
2. Back to menu
3. Play battle #2 (different names/anomalies)
4. Verify DB shows multiple entries
✓ Stats accumulate correctly
```

---

## ✅ FINAL CHECKLIST

- [ ] Full gameplay flow works (no crashes)
- [ ] Damage calculation correct
- [ ] Defend reduces damage 50%
- [ ] Skill cooldown blocks & decrements
- [ ] Item cooldown works (2 turns)
- [ ] Heal item restores HP
- [ ] HP bars update real-time
- [ ] HP bar colors change (green→yellow→red)
- [ ] Turn indicator shows current player
- [ ] Skill/Item dropdowns show cooldown info
- [ ] Winner correctly detected (HP ≤ 0)
- [ ] MessageBox shows winner
- [ ] Database saved correctly
- [ ] Player stats incremented
- [ ] Multiple rounds playable
- [ ] No unhandled exceptions

**ALL TESTS PASS = READY FOR SESI 5 (Polish & Deploy)** ✨

