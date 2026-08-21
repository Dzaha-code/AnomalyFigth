-- Migration: Add Damage column to Skill table
-- Created for fixing IndexOutOfRangeException when reading Skill data

ALTER TABLE Skill ADD COLUMN Damage INT NOT NULL DEFAULT 0;

-- Verify the column was added
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='Skill' AND COLUMN_NAME='Damage';
