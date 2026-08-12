# 🚀 SESI 4 - QUICK START GUIDE
## Gameplay Full Testing - Mulai dari Sini!

---

## ⚡ TL;DR - SINGLE COMMAND TO TEST

```bash
cd C:\Users\rafs loq\source\repos\AnomalyFigth\AnomalyFigth\WinFormsApp1
dotnet run
```

Then follow the **Testing Checklist below** while playing!

---

## 📖 DOKUMENTASI AVAILABLE

After pulling latest code, you have 3 testing guides:

### 1. **SESI_4_TESTING_GUIDE.md** (Lengkap)
**For:** Understanding ALL test scenarios in detail
- 12 tahap testing lengkap
- Expected UI & flow per stage
- Troubleshooting jika error
- Database verification
- Multiple battles testing

**When to use:** Jika ada bug atau want comprehensive understanding

### 2. **TESTING_CHECKLIST.md** (Ringkas)
**For:** Quick reference while playing
- 6 battle TEST sections
- Expected results per action
- Verification items
- Final checklist

**When to use:** Main guide saat testing gameplay

### 3. **INTERACTIVE_TESTING.md** (Step-by-step)
**For:** Guided walkthrough dengan timing
- Pre-flight checks
- 8 Phases dengan substeps
- Checkpoints after each phase
- Observable changes per action

**When to use:** First time testing atau beginner

---

## ✅ GAME FLOW TO TEST

```
START
  ↓
1. MainMenuForm
   └─ Click "Mulai Game"
	  ↓
2. PlayerNameForm
   └─ Input "Pemain1" & "Pemain2"
	  └─ Click "Lanjut"
		 ↓
3. AnomalySelectionForm
   └─ Select 2 different anomalies
	  └─ Click "Lanjut"
		 ↓
4. ItemSelectionForm
   └─ Select 2 items each player
	  └─ Click "Mulai Battle"
		 ↓
5. FormBattle ★ MAIN TESTING HAPPENS HERE
   ├─ Test Attack
   ├─ Test Skill + Cooldown
   ├─ Test Defend (50% reduction)
   ├─ Test Item (heal/boost)
   ├─ Watch HP bar colors change
   └─ Continue until Winner found
	  ↓
6. Winner Announcement
   └─ MessageBox shows result
	  └─ Database saves
		 ↓
7. Return to MainMenuForm
   └─ TEST COMPLETE ✓
```

---

## 🎮 SIMPLE BATTLE TESTING

Just play normally and CHECK:

| Feature | How to Test | Expected Result |
|---------|------------|-----------------|
| **Attack** | Click [⚔ Attack] | Damage = ATK - DEF, log shows damage |
| **Skill** | Select → Click [✨ Skill] | Damage applied, cooldown shown [CD: X] |
| **Defend** | Click [🛡 Defend] | Next damage is -50%, log mentions this |
| **Item** | Select → Click [🎒 Item] | Effect applied (heal or boost) |
| **Cooldown** | Use skill, next turn | Skill grayed, shows [CD: 2 turn] |
| **HP Bar** | Watch as HP drops | Color: GREEN > YELLOW > RED |
| **Turn** | After each action | Turn switches P1 ↔ P2 |
| **Win** | HP ≤ 0 | Winner announced, DB saved |

---

## 📋 QUICK CHECKLIST (Copy-Paste)

```
PRE-LAUNCH:
☐ Database running (phpMyAdmin)
☐ Assets/Images/placeholder.png exists
☐ Project builds: dotnet build -c Debug

GAMEPLAY:
☐ MainMenu loads
☐ Input player names OK
☐ Select anomalies OK
☐ Select items (2 each) OK
☐ Battle starts
☐ Attack works
☐ Skill works + cooldown visible
☐ Defend reduces damage
☐ Item effect works
☐ HP bars update
☐ HP bar colors change (green→yellow→red)
☐ Winner detected at HP ≤ 0
☐ MessageBox shows winner

DATABASE:
☐ MatchHistory table has new row
☐ Winner ID correct
☐ Loser ID correct
☐ Anomaly names correct
☐ Timestamp is recent

FINAL:
☐ No crashes
☐ Can play again from menu
☐ Multiple battles accumulate stats
```

---

## 🐛 IF SOMETHING BREAKS

### Game won't start
```
Solution:
1. dotnet restore
2. dotnet build -c Debug
3. Re-run: dotnet run
```

### Image doesn't load
```
Expected: Shows placeholder.png (gray box)
That's OK for now! Add anomaly images later.
```

### Damage calculation seems wrong
```
Check:
- Calculation: Player1.ATK - Player2.DEF = damage
- Defend active? Divide by 2
- Bug? Note the numbers and check BattleManager.cs
```

### Database not saving
```
Check:
1. phpMyAdmin: database anomaly_versus_db exists?
2. App.config: connection string correct?
3. Player table exists?
4. MatchHistory table exists?
5. Battle log: says "✓ Match history saved"?
```

### Battle stuck / no next turn
```
Solution:
1. Close form
2. Click "Mulai Game" again
3. Note previous battle info
4. Continue testing
```

---

## 🎯 GOAL

By end of Sesi 4 testing:

✅ **Full gameplay works** - no crashes
✅ **All mechanics work** - damage, cooldown, boost, heal
✅ **Database saves** - MatchHistory & Player stats update
✅ **Multiple rounds** - can play again

**If ALL these pass → Ready for Sesi 5! 🚀**

---

## 📊 EXPECTED DATABASE STATE

After ONE complete battle where "Pemain1" beats "Pemain2":

**Table: Player**
```
Pemain1: TotalWins = 1, TotalLosses = 0
Pemain2: TotalWins = 0, TotalLosses = 1
```

**Table: MatchHistory**
```
1 row with:
- WinnerPlayerID = Pemain1's ID
- LoserPlayerID = Pemain2's ID  
- WinnerAnomalyName = (Pemain1's picked anomaly)
- LoserAnomalyName = (Pemain2's picked anomaly)
- PlayedAt = (current timestamp)
```

---

## 💡 TIPS FOR TESTING

1. **Take notes** - if you find weird behavior, note it with HP values
2. **Test extreme cases** - what happens if damage = 0? HP > max?
3. **Alternate actions** - don't just attack every turn, mix it up
4. **Watch logs** - battle log tells you what's happening
5. **Check DB after** - phpmyadmin is your friend
6. **Play multiple rounds** - stats should accumulate
7. **Have fun!** - this is a game! Play it normally

---

## 📞 IF YOU'RE STUCK

Check these docs (in `WinFormsApp1/WinFormsApp1/`):
1. **INTERACTIVE_TESTING.md** - step-by-step walkthrough
2. **TESTING_CHECKLIST.md** - per-action verification
3. **SESI_4_TESTING_GUIDE.md** - detailed scenarios + troubleshooting
4. Code files: BattleManager.cs, FormBattle.cs

---

## 🎬 YOU'RE READY!

## Enjoy playing Anomaly Versus! Good luck! 🎮✨

**Branch:** dev  
**Status:** Ready for gameplay testing  
**Next:** Sesi 5 - Polish & any bug fixes  

