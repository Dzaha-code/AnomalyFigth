-- =========================================================
-- Schema Database: Anomaly Versus
-- Untuk MySQL (Laragon + phpMyAdmin)
-- Cara pakai: Import file ini lewat phpMyAdmin
-- =========================================================

CREATE DATABASE IF NOT EXISTS anomaly_versus_db;
USE anomaly_versus_db;

-- ---------------------------------------------------------
-- Tabel: Anomaly
-- ---------------------------------------------------------
CREATE TABLE Anomaly (
    AnomalyID     INT AUTO_INCREMENT PRIMARY KEY,
    Name          VARCHAR(50) NOT NULL,
    Role          VARCHAR(20) NOT NULL,      -- Attacker / Defender / Support
    BaseHP        INT NOT NULL,
    BaseATK       INT NOT NULL,
    BaseDEF       INT NOT NULL,
    BaseSPD       INT NOT NULL,
    Description   VARCHAR(255),
    SpritePath    VARCHAR(255)
);

-- ---------------------------------------------------------
-- Tabel: Skill
-- ---------------------------------------------------------
CREATE TABLE Skill (
    SkillID       INT AUTO_INCREMENT PRIMARY KEY,
    AnomalyID     INT NOT NULL,
    Name          VARCHAR(50) NOT NULL,
    SkillType     VARCHAR(20) NOT NULL,      -- Damage / Heal / Buff / Debuff
    Power         FLOAT NOT NULL,
    Cooldown      INT NOT NULL,
    Description   VARCHAR(255),
    CONSTRAINT fk_skill_anomaly FOREIGN KEY (AnomalyID)
        REFERENCES Anomaly(AnomalyID)
        ON DELETE CASCADE
);

-- ---------------------------------------------------------
-- Tabel: Item
-- ---------------------------------------------------------
CREATE TABLE Item (
    ItemID        INT AUTO_INCREMENT PRIMARY KEY,
    Name          VARCHAR(50) NOT NULL,
    EffectType    VARCHAR(20) NOT NULL,      -- ATK / DEF / HP / Regen, dst
    EffectValue   FLOAT NOT NULL,
    IsPercentage  TINYINT(1) NOT NULL DEFAULT 0,  -- 0 = nilai tetap, 1 = persentase
    Description   VARCHAR(255),
    IconPath      VARCHAR(255)
);

-- ---------------------------------------------------------
-- Tabel: Player
-- ---------------------------------------------------------
CREATE TABLE Player (
    PlayerID      INT AUTO_INCREMENT PRIMARY KEY,
    PlayerName    VARCHAR(50) NOT NULL,
    TotalWins     INT NOT NULL DEFAULT 0,
    TotalMatches  INT NOT NULL DEFAULT 0
);

-- ---------------------------------------------------------
-- Tabel: MatchHistory
-- ---------------------------------------------------------
CREATE TABLE MatchHistory (
    MatchID       INT AUTO_INCREMENT PRIMARY KEY,
    Player1ID     INT NOT NULL,
    Player2ID     INT NOT NULL,
    Anomaly1ID    INT NOT NULL,
    Anomaly2ID    INT NOT NULL,
    WinnerID      INT NOT NULL,
    MatchDate     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Notes         VARCHAR(255),
    CONSTRAINT fk_match_player1 FOREIGN KEY (Player1ID) REFERENCES Player(PlayerID),
    CONSTRAINT fk_match_player2 FOREIGN KEY (Player2ID) REFERENCES Player(PlayerID),
    CONSTRAINT fk_match_anomaly1 FOREIGN KEY (Anomaly1ID) REFERENCES Anomaly(AnomalyID),
    CONSTRAINT fk_match_anomaly2 FOREIGN KEY (Anomaly2ID) REFERENCES Anomaly(AnomalyID),
    CONSTRAINT fk_match_winner FOREIGN KEY (WinnerID) REFERENCES Player(PlayerID)
);

-- ---------------------------------------------------------
-- Tabel: MatchItemUsed (opsional, sesuai GDD 4.7)
-- ---------------------------------------------------------
CREATE TABLE MatchItemUsed (
    MatchItemID   INT AUTO_INCREMENT PRIMARY KEY,
    MatchID       INT NOT NULL,
    PlayerID      INT NOT NULL,
    ItemID        INT NOT NULL,
    CONSTRAINT fk_matchitem_match FOREIGN KEY (MatchID) REFERENCES MatchHistory(MatchID) ON DELETE CASCADE,
    CONSTRAINT fk_matchitem_player FOREIGN KEY (PlayerID) REFERENCES Player(PlayerID),
    CONSTRAINT fk_matchitem_item FOREIGN KEY (ItemID) REFERENCES Item(ItemID)
);

-- =========================================================
-- Data awal (dummy data) — sesuai GDD minimal 5-6 Anomaly, 6-8 Item
-- Silakan sesuaikan/tambah sesuai kebutuhan tim
-- =========================================================

INSERT INTO Anomaly (Name, Role, BaseHP, BaseATK, BaseDEF, BaseSPD, Description, SpritePath) VALUES
('Ferrox', 'Attacker', 100, 25, 10, 15, 'Anomaly agresif dengan serangan tinggi.', 'ferrox.png'),
('Terravox', 'Defender', 150, 12, 25, 8, 'Anomaly bertahan dengan HP dan DEF besar.', 'terravox.png'),
('Aquilis', 'Support', 90, 15, 12, 18, 'Anomaly support dengan skill pemulihan.', 'aquilis.png'),
('Voltrix', 'Attacker', 95, 28, 8, 20, 'Anomaly cepat dengan damage tinggi.', 'voltrix.png'),
('Umbrion', 'Defender', 140, 14, 22, 10, 'Anomaly dengan skill status effect.', 'umbrion.png'),
('Lumina', 'Support', 85, 13, 14, 16, 'Anomaly support dengan buff tim.', 'lumina.png');

INSERT INTO Skill (AnomalyID, Name, SkillType, Power, Cooldown, Description) VALUES
(1, 'Flame Slash', 'Damage', 1.5, 2, 'Serangan api dengan damage besar.'),
(2, 'Stone Wall', 'Buff', 1.2, 3, 'Meningkatkan DEF sementara.'),
(3, 'Healing Wave', 'Heal', 1.3, 3, 'Memulihkan HP.'),
(4, 'Thunder Strike', 'Damage', 1.6, 2, 'Serangan petir cepat.'),
(5, 'Shadow Bind', 'Debuff', 1.0, 3, 'Menurunkan SPD lawan.'),
(6, 'Radiant Shield', 'Buff', 1.1, 2, 'Memberi shield sementara.');

INSERT INTO Item (Name, EffectType, EffectValue, IsPercentage, Description, IconPath) VALUES
('Iron Amulet', 'DEF', 10, 0, 'Menambah DEF tetap.', 'iron_amulet.png'),
('Berserker Fang', 'ATK', 15, 0, 'Menambah ATK tetap.', 'berserker_fang.png'),
('Vitality Core', 'HP', 20, 1, 'Menambah HP berupa persentase.', 'vitality_core.png'),
('Swift Boots', 'SPD', 5, 0, 'Menambah kecepatan.', 'swift_boots.png'),
('Regen Charm', 'Regen', 5, 0, 'Memulihkan HP tiap giliran.', 'regen_charm.png'),
('Guardian Plate', 'DEF', 15, 1, 'Menambah DEF berupa persentase.', 'guardian_plate.png'),
('Power Crystal', 'ATK', 10, 1, 'Menambah ATK berupa persentase.', 'power_crystal.png'),
('Lucky Coin', 'HP', 10, 0, 'Menambah HP tetap.', 'lucky_coin.png');
