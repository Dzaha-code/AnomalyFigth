# 🎬 SESI 4: Interactive Testing Walkthrough
## Run this file step-by-step sambil meng-observe aplikasi

---

## 📌 PRE-FLIGHT CHECKS

Sebelum start testing, pastikan:

```
✓ STEP 1: Database connected
  - Open phpMyAdmin
  - Check database: anomaly_versus_db
  - Count rows: anomaly (6+), skill (6+), item (8+)

✓ STEP 2: Assets ready
  - Check folder: Assets/Images/placeholder.png
  - Status: If images exist, great. If not, will use placeholder

✓ STEP 3: Project builds
  - Run: dotnet build -c Debug
  - Result: "Build succeeded"
```

---

## 🎮 TESTING SESSION

### **Session Setup**

**Player Names (untuk consistent testing):**
- Player 1: "Pemain1"
- Player 2: "Pemain2"

**Anomalies to Select:**
- P1: Pick first one (e.g., "Ignis")
- P2: Pick different (e.g., "Aqua")

**Items to Select:**
- P1: Pick first 2 items
- P2: Pick last 2 items

---

## ⏱️ TIMING GUIDE

Expected time per step: ~30 seconds
Total gameplay: ~3-5 minutes for full battle

---

## PHASE 1: STARTUP (30 sec)

### Step 1.1: Launch Application
```
Terminal:
$ cd WinFormsApp1
$ dotnet run

Expected:
⏱️ Wait ~3-5 seconds
📋 MainMenuForm appears with:
   - Title: "ANOMALY VERSUS"
   - 2 buttons: "Mulai Game", "Keluar"
   - Window centered on screen

✓ Click: "Mulai Game"
```

**Checkpoint 1:** ✓ MainMenuForm loaded, no crashes

---

## PHASE 2: PLAYER SETUP (1 min)

### Step 2.1: Input Player Names
```
Form: PlayerNameForm

Action Sequence:
1. txtPlayer1Name.Text = "Pemain1"
2. txtPlayer2Name.Text = "Pemain2"
3. Click: "Lanjut"

Expected:
✓ MessageBox: "Pemain terdaftar!"
✓ Form navigate → AnomalySelectionForm

Debug Notes:
- Check: GameSession.Player1Name = "Pemain1"
- Check: GameSession.Player2Name = "Pemain2"
```

**Checkpoint 2:** ✓ Player names saved, database insert/update

---

## PHASE 3: ANOMALY SELECTION (1 min)

### Step 3.1: Select Anomalies
```
Form: AnomalySelectionForm

Action Sequence:
1. Click first item in listBox (e.g., "Ignis")
2. Click: "Pilih Player 1"
   Expected: Green text shows "Player 1 (Pemain1): Ignis"

3. Click different item (e.g., "Aqua")
4. Click: "Pilih Player 2"
   Expected: Green text shows "Player 2 (Pemain2): Aqua"

5. Click: "Lanjut"

Expected:
✓ Form navigate → ItemSelectionForm

Debug Notes:
- Check: GameSession.Player1Anomaly.Name = "Ignis"
- Check: GameSession.Player2Anomaly.Name = "Aqua"
```

**Checkpoint 3:** ✓ Anomalies selected, GameSession updated

---

## PHASE 4: ITEM SELECTION (1 min)

### Step 4.1: Select Player 1 Items
```
Form: ItemSelectionForm
Instruction: "Pilih 2 item untuk Player 1 (Pemain1)"

Action Sequence:
1. Check first 2 checkboxes
2. Click: "Pilih Untuk Player 1"

Expected:
✓ MessageBox: "Player 1 memilih: [Item1 Name], [Item2 Name]"
✓ Form updates:
   - Instruction changes to Player 2
   - Button changes to "Pilih Untuk Player 2"
   - Label shows "Player 1: 2 item dipilih"
```

### Step 4.2: Select Player 2 Items
```
Action Sequence:
1. Check exactly 2 checkboxes (different items)
2. Click: "Pilih Untuk Player 2"

Expected:
✓ MessageBox: "Player 2 memilih: ..."
✓ Button "Mulai Battle" becomes ENABLED
✓ Label shows "Player 2: 2 item dipilih"
```

### Step 4.3: Start Battle Confirmation
```
Action Sequence:
1. Click: "Mulai Battle"

Expected:
✓ Confirmation dialog appears with summary
✓ Klik: "Yes"
✓ Form navigate → FormBattle

Note: Kalo klik "No", kembali ke item selection
```

**Checkpoint 4:** ✓ Items selected, ready for battle

---

## PHASE 5: BATTLE START (5-10 min)

### Step 5.0: Verify Initial State
```
Form: FormBattle

Initial UI Check:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

LEFT PANEL (Player 1):
 ✓ [Picture: Ignis image or placeholder]
 ✓ Label: "Pemain1"
 ✓ Label: "Ignis"
 ✓ Label: "HP: 100 / 100"
 ✓ ProgressBar: full (green)

CENTER PANEL (Log):
 ✓ Label: "⏳ Giliran: Pemain1 (Player 1)"
 ✓ ListBox content:
   - "=== ANOMALY VERSUS BATTLE START ==="
   - "Pemain1 menggunakan Ignis"
   - "Pemain2 menggunakan Aqua"

RIGHT PANEL (Player 2):
 ✓ [Picture: Aqua image or placeholder]
 ✓ Label: "Pemain2"
 ✓ Label: "Aqua"
 ✓ Label: "HP: 120 / 120"
 ✓ ProgressBar: full (green)

ACTION PANEL (Bottom):
 ✓ Button: "⚔ Attack" [ENABLED]
 ✓ Dropdown: Skill list (Ignis skills)
 ✓ Button: "✨ Pakai Skill" [ENABLED]
 ✓ Button: "🛡 Defend" [ENABLED]
 ✓ Dropdown: Item list (2 items)
 ✓ Button: "🎒 Pakai Item" [ENABLED]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✓ Click: "⚔ Attack"
```

**Checkpoint 5.0:** ✓ Battle UI loaded correctly

---

### Step 5.1: Player 1 ACTION - ATTACK
```
Current Turn: Player 1 (Pemain1)

Action:
1. Click: [⚔ Attack]

Observable Changes:
✓ Log add new line:
  "Pemain1 menyerang dengan serangan biasa!"
  "Damage: [calculated value]"

✓ Player 2 HP decreases
  Example: 120 → 110 (if damage = 10)

✓ Player 2 HP Bar updates
  Color: still GREEN (unless very low)

✓ Turn indicator changes:
  "⏳ Giliran: Pemain2 (Player 2)"

✓ Skill dropdown switches to AQUA skills
✓ Item dropdown switches to AQUA items

✓ (Now it's Player 2's turn)
```

**Checkpoint 5.1:** ✓ Attack action works, damage applied

---

### Step 5.2: Player 2 ACTION - ATTACK or SKILL
```
Current Turn: Player 2 (Pemain2)

ACTION OPTION A - ATTACK:
1. Click: [⚔ Attack]
   Expected: Same as Step 5.1, but damage to Ignis

ACTION OPTION B - SKILL:
1. Select skill dari dropdown
2. Click: [✨ Pakai Skill]
   Expected:
   ✓ Log show: "Pemain2 menggunakan skill [Name]!"
   ✓ Log show: "Damage: [value]"
   ✓ Log show: "Cooldown: [X] turn"
   ✓ Skill in dropdown shows: "[Name] [CD: X turn]"
   ✓ HP Ignis decreases more than attack

ACTION OPTION C - ITEM:
1. Select item dari dropdown
2. Click: [🎒 Pakai Item]
   Expected:
   ✓ Log show: "Pemain2 menggunakan item [Name]!"
   ✓ If "heal": HP increase
   ✓ If "boost": Next attacks stronger
   ✓ Item shows: "[Name] [CD: 2 turn]"

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

RECOMMENDATION: Alternate attacks & skills
to test different mechanics
```

**Checkpoint 5.2:** ✓ Different action types work

---

### Step 5.3: TEST - DEFEND
```
Goal: Test defend mechanic (50% damage reduction)

ACTION SEQUENCE:

1. Player 1 turn:
   - Click: [🛡 Defend]
   - Log: "Pemain1 mengambil stance pertahanan!..."
   - Turn → Player 2

2. Player 2 turn:
   - Click: [⚔ Attack]
   - Note damage amount

OBSERVATION:
✓ Damage should be HALF of normal
  Example: Normal damage 20 → With defend 10
✓ Log mention: "...dikurangi 50%..."

3. Continue to next turn
   - Player 1 defend status ends
   - Normal damage resumes
```

**Checkpoint 5.3:** ✓ Defend reduces damage correctly

---

### Step 5.4: TEST - COOLDOWN MECHANICS
```
Goal: Verify skill cooldown blocks & decrements

SETUP:
1. Player with "Cooldown: 2" skill ready
2. Select & use skill
3. Observe dropdown next turn

OBSERVATION SEQUENCE:

Turn A (P1):
  - Use skill "Water Blast" (CD: 2)
  - Log: "Cooldown: 2 turn"

Turn B (P2):
  - (just act normally)

Turn C (P1):
  - Dropdown show: "Water Blast [CD: 2 turn]"
  - (can't use, skill disabled)

Turn D (P2):
  - (just act normally)

Turn E (P1):
  - Dropdown show: "Water Blast [CD: 1 turn]" or ready?
  - Depending on implementation

Turn F (P2):
  Wait...

Turn G (P1):
  - "Water Blast" should be READY (no [CD: ...])
  - Can use again

CHECK:
✓ Skill disabled during CD
✓ CD decrements each turn
✓ Skill enabled when CD reaches 0
```

**Checkpoint 5.4:** ✓ Cooldown system tracks correctly

---

### Step 5.5: HP BAR COLOR TEST
```
Goal: Verify HP bar changes color based on percentage

Monitor During Battle:

PHASE 1: HP > 50% (GREEN)
  Example: P1 HP 80/100 = 80%
  ✓ Bar color should be: LIME GREEN

PHASE 2: HP 25-50% (YELLOW)
  Example: P1 HP 40/100 = 40%
  ✓ Bar color should be: YELLOW

PHASE 3: HP < 25% (RED)
  Example: P1 HP 20/100 = 20%
  ✓ Bar color should be: RED

ACTION: Continue battle until HP drops low
OBSERVE: Color transitions smoothly
```

**Checkpoint 5.5:** ✓ HP bar colors change correctly

---

### Step 5.6: EXTEND BATTLE - CONTINUE ACTIONS
```
Goal: Keep playing until someone wins

ACTION SEQUENCE:
1. Both players keep attacking/defending/using items
2. Watch HP counts
3. Continue for ~5-10 more turns
4. Monitor:
   ✓ Turns alternate correctly
   ✓ Log keeps updating
   ✓ No crashes or lag
   ✓ HP bars update real-time

OPTIONAL TESTS:
- Use same item twice → verify 2-turn CD
- Use boost item → damage increases next turn
- Mix defend + skill to see combined effects
```

**Ongoing:** ✓ Battle mechanics stable

---

## PHASE 6: BATTLE END (30 sec)

### Step 6.1: Win Condition Triggered
```
SCENARIO:
Player 1 HP: 10/100
Player 2 HP: 50/120
Player 2's turn → Attack or Skill

If Damage ≥ 10:
  Player 1 HP → ≤ 0
  Battle END!

Expected Behavior:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

IMMEDIATE:
✓ Log show: "=== BATTLE END ==="
✓ Winner announcement in log
✓ ALL BUTTONS DISABLED
✓ No more actions possible

✓ MessageBox appears:
  ┌─────────────────────────┐
  │ 🏆 Pemain2 MENANG!    │
  │ Aqua mengalahkan Ignis  │
  │      [OK]               │
  └─────────────────────────┘

✓ Click [OK]

✓ Log update: "✓ Match history saved..."
✓ FormBattle closes
✓ Return to MainMenuForm
```

**Checkpoint 6.1:** ✓ Winner detected, DB saved

---

## PHASE 7: DATABASE VERIFICATION (2 min)

### Step 7.1: Verify MatchHistory
```
Tool: phpMyAdmin

Navigation:
1. Select Database: anomaly_versus_db
2. Select Table: MatchHistory
3. Click: Browse (or SELECT *)

Expected New Row:
┌──────────────────────────────────────────┐
│ MatchID | WinnerPlayerID | LoserPlayerID │
│ 1       | [P2 ID]        | [P1 ID]       │
├──────────────────────────────────────────┤
│ WinnerAnomalyName | LoserAnomalyName     │
│ "Aqua"            | "Ignis"              │
├──────────────────────────────────────────┤
│ PlayedAt                                 │
│ 2024-01-15 10:30:45 (current time)      │
└──────────────────────────────────────────┘

✓ Verify all fields correct
✓ Timestamp is recent (within last minute)
```

### Step 7.2: Verify Player Stats
```
Table: Player

Expected Updates:
┌─────────────┬──────────┬─────────────┐
│ PlayerName  │ Wins     │ Losses      │
├─────────────┼──────────┼─────────────┤
│ Pemain2     │ +1 ✓     │ 0           │
│ Pemain1     │ 0        │ +1 ✓        │
└─────────────┴──────────┴─────────────┘

Verify:
✓ Winner's Wins field incremented
✓ Loser's Losses field incremented
```

**Checkpoint 7:** ✓ Database updated correctly

---

## PHASE 8: MULTIPLE ROUNDS TEST (Optional, 3-5 min)

### Step 8.1: Play Another Round
```
Current State: Returned to MainMenuForm

ACTION:
1. Click: "Mulai Game"
2. Enter DIFFERENT player names:
   Player 1: "Pemain3"
   Player 2: "Pemain4"
3. Select DIFFERENT anomalies & items
4. Play another battle to completion

VERIFY:
✓ All new data correct in database
✓ Previous match still intact
✓ Stats accumulate (not overwrite)

Example After 2 Battles:
  Pemain2: Wins=2, Losses=0 (if won both)
  OR
  Pemain2: Wins=1, Losses=1 (if split)
```

**Checkpoint 8:** ✓ Multiple rounds work correctly

---

## ✅ FINAL VALIDATION

After all testing, verify:

```
CODE QUALITY:
✓ No unhandled exceptions
✓ No crashes
✓ Smooth animations / transitions
✓ Responsive UI (no freezing)

GAMEPLAY:
✓ Damage calculations correct
✓ Defend reduces 50%
✓ Cooldowns blocking & decrementing
✓ Items apply correct effects
✓ Turn order correct
✓ Winner detected at HP ≤ 0

DATABASE:
✓ MatchHistory created
✓ Player stats updated
✓ Multiple rounds tracked
✓ No duplicate entries

USER EXPERIENCE:
✓ Clear win/lose announcement
✓ Battle log readable
✓ HP bars intuitive
✓ Easy navigation
✓ No confusing error messages
```

---

## 📊 RESULT SUMMARY

If ALL checkpoints passed:
```
✨ SESI 4 - COMPLETE! ✨
Project ready for Sesi 5 (Polish & Deployment)
```

If ANY checkpoint failed:
```
🐛 Debug Notes:
- Note which checkpoint failed
- Check related code
- Add Debug.WriteLine() for tracing
- Re-run from that checkpoint
```

---

**Happy Testing! Good luck Anomaly Versus team! 🎮🚀**

