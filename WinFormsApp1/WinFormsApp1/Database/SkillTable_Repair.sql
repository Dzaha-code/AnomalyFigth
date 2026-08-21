-- ============================================
-- SKILL TABLE VERIFICATION & REPAIR
-- ============================================

-- 1. CEK struktur table Skill yang ada
DESC Skill;

-- 2. JIKA Damage column BELUM ADA, tambahkan:
ALTER TABLE Skill ADD COLUMN Damage INT NOT NULL DEFAULT 10;

-- 3. JIKA MpCost belum ada:
ALTER TABLE Skill ADD COLUMN MpCost INT NOT NULL DEFAULT 0;

-- 4. VERIFIKASI semua column yang diperlukan exist:
SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='Skill'
ORDER BY ORDINAL_POSITION;

-- 5. UPDATE sample data untuk testing:
-- Asumsikan Skill sudah punya data, update Damage & MpCost jika NULL:
UPDATE Skill SET Damage = 10 WHERE Damage IS NULL OR Damage = 0;
UPDATE Skill SET MpCost = 5 WHERE MpCost IS NULL;

-- 6. VERIFY data:
SELECT SkillID, AnomalyID, Name, SkillType, Damage, MpCost, Description 
FROM Skill 
LIMIT 5;
