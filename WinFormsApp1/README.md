# 🎮 ANOMALY VERSUS - Game Battle System
## Status: Sesi 4 - Ready for Full Gameplay Testing

---

## 📌 Project Overview

**Anomaly Versus** adalah game desktop turn-based 2-player (pass & play) battle system dibangun dengan:
- **Framework:** WinForms (.NET 10)
- **Database:** MySQL (Laragon + phpMyAdmin)
- **Architecture:** Repository Pattern + GameSession + BattleManager

---

## 📊 Progress Status

| Sesi | Milestone | Status | Driver |
|------|-----------|--------|--------|
| **1** | Database Schema + Models + Repository | ✅ Selesai | Anggota 1 |
| **2** | Menu UI + Player/Anomaly/Item Selection | ✅ Selesai | Anggota 2 |
| **3** | Battle System Logic + Battle UI | ✅ Selesai | Anggota 3 |
| **4** | Testing Gameplay Full + Bug Fix | 🔴 IN PROGRESS | Anggota 1 |
| **5** | Polish & Final Deployment | ⏳ Planned | All Team |

---

## 🚀 HOW TO RUN GAMEPLAY

### Quick Start
```bash
cd WinFormsApp1
dotnet build -c Debug
dotnet run
```

### Full Workflow
1. **Main Menu** → Klik "Mulai Game"
2. **Input Nama** → Masukkan Player 1 & Player 2
3. **Pilih Anomaly** → Setiap player pilih 1 karakter
4. **Pilih Item** → Setiap player pilih 2 support item
5. **Battle** → Attack/Skill/Defend/Item sampai salah satu menang
6. **Result** → Pemenang diumumkan, DB simpan history

### Testing Guides
Navigate to `WinFormsApp1/WinFormsApp1/` folder untuk:
- **SESI_4_START_HERE.md** ← **START HERE** (Quick guide)
- **TESTING_CHECKLIST.md** ← Quick reference
- **INTERACTIVE_TESTING.md** ← Step-by-step walkthrough
- **SESI_4_TESTING_GUIDE.md** ← Detailed + troubleshooting

---

## 📁 Project Structure

```
WinFormsApp1/
├── Models/                           # Data models
│   ├── Anomaly.cs                   # Character stats
│   ├── Skill.cs                     # Skills with cooldown
│   ├── Item.cs                      # Support items
│   ├── Player.cs                    # Player profile
│   └── MatchHistory.cs              # Battle records
│
├── DataAccess/                      # Database layer
│   ├── DBConnection.cs              # MySQL connection
│   ├── AnomalyRepository.cs         # CRUD operations
│   ├── SkillRepository.cs           # Skill queries
│   ├── ItemRepository.cs            # Item queries
│   ├── PlayerRepository.cs          # Player stats
│   └── MatchHistoryRepository.cs    # Match records
│
├── Logic/                           # Game logic
│   └── BattleManager.cs             # Battle mechanics (★ CORE)
│
├── GameSession.cs                   # Global session data
│
├── Forms/
│   ├── MainMenuForm.cs              # Main menu
│   ├── PlayerNameForm.cs            # Player input
│   ├── AnomalySelectionForm.cs      # Character select
│   ├── ItemSelectionForm.cs         # Item select
│   └── FormBattle.cs                # Battle UI (★ MAIN GAMEPLAY)
│
├── Assets/
│   └── Images/
│       ├── placeholder.png          # Fallback image
│       └── Anomaly/                 # Character pictures
│
└── Testing Docs/
	├── SESI_4_START_HERE.md         # (←) START HERE
	├── TESTING_CHECKLIST.md
	├── INTERACTIVE_TESTING.md
	└── SESI_4_TESTING_GUIDE.md
```

---

## ⚙️ Battle System Mechanics

### Damage Calculation
```
Damage = Attacker.ATK - Defender.DEF (min 1)
If Defending: Damage = Damage / 2 (floor division)
```

### Actions Available per Turn
| Action | Effect | Cooldown |
|--------|--------|----------|
| **Attack** | Damage = ATK - DEF | None |
| **Skill** | Damage = Skill.Damage | Skill.MpCost turn |
| **Defend** | -50% incoming damage | 1 turn (current turn only) |
| **Item** | Heal / Boost | 2 turns (hardcoded) |

### Boost Effects
- **Heal:** +EffectValue to HP (capped at MaxHP)
- **Attack Boost:** +EffectValue ATK for 2 turns
- **Defense Boost:** +EffectValue DEF for 2 turns

### Win Condition
- HP ≤ 0 = opponent wins
- Match automatically saved to database
- Player stats (Wins/Losses) incremented

---

## 🗄️ Database Schema

### Tables
```
Player              MatchHistory        Anomaly
├─ PlayerID        ├─ MatchID          ├─ AnomalyID
├─ PlayerName      ├─ WinnerPlayerID   ├─ Name
├─ TotalWins       ├─ LoserPlayerID    ├─ Role
├─ TotalLosses     ├─ WinnerAnomalyName├─ BaseHP
└─ TotalMatches    ├─ LoserAnomalyName ├─ BaseATK
				   ├─ PlayedAt         ├─ BaseDEF
				   └─ (more...)        └─ BaseSPD

Skill              Item
├─ SkillID         ├─ ItemID
├─ AnomalyID       ├─ Name
├─ Name            ├─ EffectType
├─ Damage          ├─ EffectValue
└─ MpCost          └─ IsPercentage
```

---

## 🎮 GAMEPLAY TESTING CHECKLIST

### Pass All These Checks:

**Mechanics**
- [ ] Attack damage = ATK - DEF
- [ ] Defend reduces damage 50%
- [ ] Skill cooldown blocks & decrements
- [ ] Item cooldown 2 turns works
- [ ] Heal item restores HP correctly
- [ ] Boost items increase ATK/DEF for 2 turns
- [ ] Turn alternates correctly (P1 → P2 → P1)

**UI/UX**
- [ ] HP bars update real-time
- [ ] HP bar colors change (Green > 50%, Yellow 25-50%, Red < 25%)
- [ ] Battle log shows all actions clearly
- [ ] Skill dropdown shows cooldown [CD: X turn]
- [ ] Item dropdown shows cooldown [CD: X turn]
- [ ] Turn indicator shows current player

**Database**
- [ ] MatchHistory record created
- [ ] Winner & Loser IDs correct
- [ ] Anomaly names saved correctly
- [ ] Player win/loss stats incremented
- [ ] Timestamp recorded

**Navigation**
- [ ] MainMenu → PlayerName → Anomaly → Item → Battle
- [ ] Battle result shown & saved
- [ ] Return to MainMenu works
- [ ] Can play multiple rounds

---

## 🐛 Known Issues & Fixes

### Image Loading
**Status:** Expected placeholder.png used if image not found
**Fix:** Add anomaly images to `Assets/Images/Anomaly/{Name}.png` (match database Anomaly.Name)

### Cooldown Display
**Status:** Shows [CD: X turn] in dropdown
**Enhancement:** Could grey-out disabled skills for clarity

### Boost Effects
**Status:** Applied internally, visible in damage numbers
**Enhancement:** Could show status bar indicators

---

## 📝 Code Quality

- ✅ No compilation errors
- ✅ No unhandled exceptions (on normal gameplay)
- ✅ Repository pattern for data access
- ✅ GameSession for cross-form data sharing
- ✅ BattleManager for game logic separation
- ✅ Clean UI/Logic separation
- ⚠️ No async/await (not needed for this scope)
- ⚠️ No LINQ advanced (kept simple)

---

## 🔄 Git Workflow

### Branches
- **main** - Production ready (future)
- **dev** - Current development (Sesi 1-4)
- **feature/*** - Feature branches (as needed)

### Latest Commits
```
4957650 [Sesi 4] Quick Start Guide - Ready for gameplay testing
e9a5b4b [Sesi 4] Testing Documentation - Complete gameplay guide
d82791d [Sesi 3] Implementasi Battle System - Anggota 3
```

### How to Pull Latest
```bash
git pull origin dev
cd WinFormsApp1
dotnet run
```

---

## 👥 Team Roles

| Anggota | Role | Sesi | Deliverable |
|---------|------|------|-------------|
| **1** | Driver - DB & Logic | 1, 4, 5 | Schema, Repositories, Testing |
| **2** | Driver - UI/Selection | 2 | Forms, Player/Anomaly/Item Select |
| **3** | Driver - Battle | 3 | BattleManager, FormBattle UI |

---

## ✨ Next Steps (Sesi 5)

After testing complete:
1. **Bug Fixes** - Fix any issues found in Sesi 4
2. **Polish UI** - Better visuals, animations
3. **Add Sounds** (optional) - SFX/BGM
4. **Final Testing** - Full integration test
5. **Deployment** - Package for distribution

---

## 📚 Learning Resources

**Files to Study:**
1. `Logic/BattleManager.cs` - Game logic
2. `FormBattle.cs` - UI + event handling
3. `GameSession.cs` - State management
4. `DataAccess/*Repository.cs` - Database patterns

**Concepts Covered:**
- WinForms UI patterns
- Repository pattern
- Game state management
- Database integration
- Event-driven programming

---

## 💬 Questions?

1. Check documentation files (TESTING_CHECKLIST.md, etc)
2. Review related source code
3. Add Debug.WriteLine() for tracing
4. Check phpMyAdmin for DB state
5. Review git history for context

---

## 🎊 Summary

**Sesi 1-3:** COMPLETE ✅
- Database ready
- All forms built
- Battle system implemented

**Sesi 4:** IN PROGRESS 🔴
- Follow testing guide
- Play multiple battles
- Verify database saves

**Sesi 5:** PENDING ⏳
- Fix any bugs found
- Polish UI
- Final release

---

## 🚀 Status: READY FOR TESTING

**How to proceed:**
1. Read: `SESI_4_START_HERE.md`
2. Run: `dotnet run`
3. Play: Full battle flow
4. Verify: All mechanics working
5. Check: Database entries correct
6. Report: Any issues found

**Target:** Complete testing in 2-3 hours, minimal bugs

Good luck! Enjoy Anomaly Versus! 🎮✨

---

*Last Updated: Sesi 4 Testing Phase*  
*Branch: dev*  
*Status: Ready for Full Gameplay Testing*

