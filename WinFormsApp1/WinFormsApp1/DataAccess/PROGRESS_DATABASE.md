# Checklist Progres — Anggota 1 (feature/database)
### Project: Anomaly Versus — WinForms .NET Framework

---

## ✅ Sudah selesai

- [x] Buat struktur folder `Models` dan `DataAccess` di project
- [x] Setup Laragon + phpMyAdmin
- [x] Buat database `anomaly_versus_db` (6 tabel: Anomaly, Skill, Item, Player, MatchHistory, MatchItemUsed)
- [x] Buat `schema.sql` beserta dummy data (6 Anomaly, 6 Skill, 8 Item)
- [x] Install package NuGet `MySql.Data`
- [x] Setup `App.config` (connection string ke MySQL)
- [x] Buat `DBConnection.cs`
- [x] Test koneksi database berhasil
- [x] Buat 5 class Model: `Anomaly.cs`, `Skill.cs`, `Item.cs`, `Player.cs`, `MatchHistory.cs`
- [x] Buat `AnomalyRepository.cs` (Get all, Get by ID, Add, Update, Delete)
- [x] Buat `SkillRepository.cs` (Get by AnomalyID, Add)
- [x] Buat `ItemRepository.cs` (Get all, Add)
- [x] Buat `PlayerRepository.cs` (Get or create by nama, Update stats)
- [x] Buat `MatchHistoryRepository.cs` (Add match)

---

## ⬜ Belum dikerjakan (langkah selanjutnya)

- [ ] Taruh semua file Model & Repository ke folder yang benar di Solution Explorer
- [ ] Sesuaikan `namespace` kalau nama project bukan `WinFormsApp1`
- [ ] Test tiap fungsi repository satu-satu (misal panggil `GetAllAnomaly()` dari `Form1` sementara, cek data muncul)
- [ ] Tambah `Add`, `Update`, `Delete` untuk `Skill`, `Item` kalau nanti dibutuhkan form admin/edit data
- [ ] Commit bertahap per bagian (jangan digabung 1 commit besar):
  - [ ] Commit: "Tambah App.config dan koneksi MySQL"
  - [ ] Commit: "Tambah schema.sql"
  - [ ] Commit: "Tambah class Model"
  - [ ] Commit: "Tambah Repository (Anomaly, Skill, Item, Player, MatchHistory)"
- [ ] Push ke branch `feature/database`
- [ ] Buka Pull Request ke `dev`
- [ ] Minta review dari Anggota 2 atau 3
- [ ] Setelah merge, informasikan ke tim: "class Model + Repository sudah siap dipakai di `dev`"

---

## 🔜 Belum dimulai (di luar scope kamu, tapi perlu dikoordinasikan)

- [ ] Anggota 2: mulai form Menu, Seleksi Anomaly & Item (butuh `AnomalyRepository`, `ItemRepository`, `PlayerRepository` dari kamu)
- [ ] Anggota 3: mulai sistem Battle (butuh `SkillRepository`, `MatchHistoryRepository` dari kamu)
- [ ] Integrasi bertiga di akhir Minggu 4 (test jalan bareng, cek match history tersimpan benar)

---

**Catatan:** centang `[x]` → `[ ]` di file ini (edit manual) tiap kali progres bertambah, biar tim bisa lihat sekilas siapa sudah sampai mana. File ini bisa ditaruh di root repo, misal `PROGRESS_DATABASE.md`.
