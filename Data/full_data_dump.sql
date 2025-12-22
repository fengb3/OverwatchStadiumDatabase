-- Overwatch Stadium Database Full Dump
-- Generated at 2025-12-22T09:24:55.5918401Z
-- Note: This script is designed to be compatible with SQLite, PostgreSQL, SQL Server, and MySQL (with ANSI_QUOTES enabled).

-- SQL Server: Enable Identity Insert
/*
SET IDENTITY_INSERT "Heroes" ON;
SET IDENTITY_INSERT "Items" ON;
SET IDENTITY_INSERT "HeroExclusives" ON;
SET IDENTITY_INSERT "Buffs" ON;
SET IDENTITY_INSERT "ItemBuffs" ON;
*/

-- MySQL: Enable ANSI Quotes
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ANSI_QUOTES' */;

-- Table: Heroes
INSERT INTO "Heroes" ("Id", "Name") VALUES (1, 'D.Va');
INSERT INTO "Heroes" ("Id", "Name") VALUES (2, 'Doomfist');
INSERT INTO "Heroes" ("Id", "Name") VALUES (3, 'Hazard');
INSERT INTO "Heroes" ("Id", "Name") VALUES (4, 'Junker Queen');
INSERT INTO "Heroes" ("Id", "Name") VALUES (5, 'Orisa');
INSERT INTO "Heroes" ("Id", "Name") VALUES (6, 'Reinhardt');
INSERT INTO "Heroes" ("Id", "Name") VALUES (7, 'Sigma');
INSERT INTO "Heroes" ("Id", "Name") VALUES (8, 'Winston');
INSERT INTO "Heroes" ("Id", "Name") VALUES (9, 'Zarya');
INSERT INTO "Heroes" ("Id", "Name") VALUES (10, 'Ashe');
INSERT INTO "Heroes" ("Id", "Name") VALUES (11, 'Cassidy');
INSERT INTO "Heroes" ("Id", "Name") VALUES (12, 'Freja');
INSERT INTO "Heroes" ("Id", "Name") VALUES (13, 'Genji');
INSERT INTO "Heroes" ("Id", "Name") VALUES (14, 'Junkrat');
INSERT INTO "Heroes" ("Id", "Name") VALUES (15, 'Mei');
INSERT INTO "Heroes" ("Id", "Name") VALUES (16, 'Pharah');
INSERT INTO "Heroes" ("Id", "Name") VALUES (17, 'Reaper');
INSERT INTO "Heroes" ("Id", "Name") VALUES (18, 'Sojourn');
INSERT INTO "Heroes" ("Id", "Name") VALUES (19, 'Soldier: 76');
INSERT INTO "Heroes" ("Id", "Name") VALUES (20, 'Torbjörn');
INSERT INTO "Heroes" ("Id", "Name") VALUES (21, 'Tracer');
INSERT INTO "Heroes" ("Id", "Name") VALUES (22, 'Ana');
INSERT INTO "Heroes" ("Id", "Name") VALUES (23, 'Brigitte');
INSERT INTO "Heroes" ("Id", "Name") VALUES (24, 'Juno');
INSERT INTO "Heroes" ("Id", "Name") VALUES (25, 'Kiriko');
INSERT INTO "Heroes" ("Id", "Name") VALUES (26, 'Lúcio');
INSERT INTO "Heroes" ("Id", "Name") VALUES (27, 'Mercy');
INSERT INTO "Heroes" ("Id", "Name") VALUES (28, 'Moira');
INSERT INTO "Heroes" ("Id", "Name") VALUES (29, 'Wuyang');
INSERT INTO "Heroes" ("Id", "Name") VALUES (30, 'Zenyatta');

-- Table: Items
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (1, 'Compensator', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '1000.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (2, 'Plasma Converter', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '1000.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (3, 'Weapon Grease', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '1000.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (4, 'Ammo Reserves', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '1500.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (5, 'Frenzy Amplifier', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '1500.0', 'Common', 'Eliminations grant 10% Attack Speed and 15% Move Speed for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (6, 'Stratosphere Beacon', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '1500.0', 'Common', 'Damage to airborne targets above 3m deals 10 bonus damage and slows the enemy by 25% for 3s (15s Cooldown)');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (7, 'Aftermarket Firing Pin', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '3750.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (8, 'Advanced Nanobiotics', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '4000.0', 'Rare', 'After healing an ally, gain 10% Attack Speed for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (9, 'Shieldbuster', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '4000.0', 'Rare', 'After dealing damage to Shields or Armor, gain 15% Attack Speed for 1s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (10, 'Stockpile', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '4000.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (11, 'Emergency Chip', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '4000.0', 'Rare', 'Once per life, when you are below 150 Life, gain 15% Weapon Lifesteal and 50 Overhealth for 5s');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (12, 'Technoleech', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '4500.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (13, 'Icy Coolant', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '5500.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (14, 'Talon Modification Module', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '6000.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (15, 'Aerial Distresser', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '9000.0', 'Epic', 'Weapon damage to airborne enemies deal 25% bonus damage over 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (16, 'Codebreaker', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '10000.0', 'Epic', 'Ignore 50% of Armor''s damage reduction.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (17, 'Salvaged Slugs', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '9000.0', 'Epic', 'Dealing Weapon Damage to Barriers or Deployables has a 50% chance to restore 1 ammo.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (18, 'Volskaya Ordnance', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '9500.0', 'Epic', 'Deal 5% increased Weapon Damage for every 100 Max Life the target has more than you, up to 25%.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (19, 'Weapon Jammer', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '10000.0', 'Epic', 'Dealing Weapon Damage negates 10% of the target''s bonus Attack Speed and increases your Attack Speed by 10% for 2s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (20, 'Amari''s Antidote', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '11000.0', 'Epic', 'While healing an ally below 50% Life with your weapon, deal 15% increased Weapon Healing.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (21, 'Commander''s Clip', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '10000.0', 'Epic', 'When you use an ability or Gadget, restore 10% of your Max Ammo.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (22, 'Booster Jets', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '11000.0', 'Epic', 'When you use an ability or Gadget, gain 10% Move Speed for 2s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (23, 'El-Sa''ka Suppressor', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '11000.0', 'Epic', 'Critical Hits apply 30% Healing Reduction to the target for 2s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (24, 'Hardlight Accelerator', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '11000.0', 'Epic', 'When you use an ability or Gadget, gain 5% Weapon Power for 3s, stacking up to 3 times.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (25, 'The Closer', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '14500.0', 'Epic', 'Critical Hits reveal the target for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (26, 'Eye of the Spider', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Weapon)', '14000.0', 'Epic', 'Deal 10% increased damage to enemies below 30% Life.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (27, 'Charged Plating', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '1000.0', 'Common', 'After you spend your Ultimate Charge, gain 25 Armor and 10% Ability Power for the rest of the round.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (28, 'Power Playbook', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '1000.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (29, 'Shady Spectacles', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '1000.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (30, 'Winning Attitude', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '1500.0', 'Common', 'When you die, gain 15% Ultimate Charge.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (31, 'Custom Stock', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '3750.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (32, 'Biolight Overflow', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '4000.0', 'Rare', 'When you spend your Ultimate Charge, cleanse yourself and grant nearby allies 75 Overhealth for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (33, 'Energized Bracers', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '4000.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (34, 'Junker Whatchamajig', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '4000.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (35, 'Skyline Nanites', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '4000.0', 'Rare', 'Ability damage to airborne enemies deal 20% bonus damage over 2s');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (36, 'Wrist Wraps', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '4000.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (37, 'Multi-Tool', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '4500.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (38, 'Vitality Amplifier', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '5000.0', 'Rare', 'While above 50% Life gain 10% Ability Power.
Otherwise, gain 15% Ability Lifesteal and abilities refresh 10% faster.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (39, 'Nano Cola', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '5500.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (40, 'Sonic Recharger', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '9000.0', 'Epic', 'Passive Life Regeneration can overheal up to 75.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (41, 'Biotech Maximizer', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '10000.0', 'Epic', 'When you use an ability that heals, reduce its cooldown by 5% for each unique ally it heals.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (42, 'Catalytic Crystal', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '10000.0', 'Epic', 'Ability Damage and Healing grants 20% more Ultimate Charge.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (43, 'Lumérico Fusion Drive', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '11000.0', 'Epic', 'When you use an ability or Gadget, restore 50 Armor or Shields over 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (44, 'Superflexor', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '10000.0', 'Epic', 'When you deal Weapon Damage or Healing, gain 5% Ability Power for 3s, stacking up to 5 times.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (45, 'Cybervenom', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '10500.0', 'Epic', 'Dealing Ability damage applies 30% healing reduction for 2s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (46, 'Three-Tap Tommygun', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '9500.0', 'Epic', 'After using an ability or Gadget, for 3 seconds Weapon Damage deals additional damage equal to 1% of the target''s Life.
(0.25s Cooldown).');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (47, 'Iridescent Iris', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '12000.0', 'Epic', 'After spending your Ultimate Charge, gain 100 Overhealth for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (48, 'Liquid Nitrogen', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '11000.0', 'Epic', 'Dealing Ability Damage slows the target''s Move Speed by 10% and their bonus Move Speed by 15% for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (49, 'Mark of the Kitsune', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '11000.0', 'Epic', 'When you use an ability or Gadget, your next instance of Weapon Damage or Healing deals  25 bonus damage or healing.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (50, 'Champion''s Kit', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Ability)', '14500.0', 'Epic', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (51, 'Ballistic Buffer', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1000.0', 'Common', 'After taking damage beyond 20m, gain 50 overhealth for 3s (15s cooldown)');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (52, 'Electrolytes', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1000.0', 'Common', 'At the start of the round and every time you respawn, gain 100 unrecoverable Overhealth.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (53, 'Field Rations', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1000.0', 'Common', 'While on the Objective, restore 8 Life every 1s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (54, 'Running Shoes', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1000.0', 'Common', 'At the start of the round and when you respawn while not in Overtime, gain 30% Move Speed for 10s while out of combat.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (55, 'Adrenaline Shot', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1500.0', 'Common', 'Health Packs grant 20% Move Speed for 5s and 50 Overhealth.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (56, 'Armored Vest', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1500.0', 'Common', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (57, 'First Aid Kit', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1500.0', 'Common', 'Reduce the time before your Life begins regenerating by 33%.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (58, 'Reflex Coating', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1500.0', 'Common', 'When you are stunned, slept, hindered or anti-healed, gain 75 Overhealth for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (59, 'Siphon Gloves', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1500.0', 'Common', '[Quick Melee] damage heals for 25 Life.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (60, 'Cushioned Padding', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '3750.0', 'Rare', 'When affected by Stun, Sleep, or Hinder, restore 10% of your max Life over 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (61, 'Iron Eyes', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '3750.0', 'Rare', 'You take 20% reduced damage from Critical Hits.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (62, 'Reinforced Titanium', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '3750.0', 'Rare', 'While you have Shields, take 15% reduced Ability Damage and gain 5% Ability Power.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (63, 'Vishkar Condensor', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '3750.0', 'Rare', 'Convert 150 Health into Shields.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (64, 'Bloodbound', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '4000.0', 'Rare', 'The last enemy to deal a final blow to you is Revealed when nearby. Deal 10% more damage to them and take 10% reduced damage from them.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (65, 'Ironclad Exhaust Ports', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '4000.0', 'Rare', 'When you use an ability or Gadget, gain 25 Overhealth for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (66, 'Vital-E-Tee', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '4000.0', 'Rare', 'Convert 100 Health into Armor.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (67, 'Crusader Hydraulics', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '4500.0', 'Rare', 'While you have Armor, take 10% reduced Weapon Damage and gain 5% Attack Speed.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (68, 'Meka Z-Series', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '5000.0', 'Rare', '');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (69, 'Geneticist''s Vial', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '9000.0', 'Epic', 'The first time you would die each round, revive instead with 250 Life after 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (70, 'Divine Intervention', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '9500.0', 'Epic', 'When you are Stunned or take more than 125 pre-mitigation damage at once, gain Overhealth equal to 20% of damage taken for 3s and start regenerating your Shields.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (71, 'Overdrive Core', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '9500.0', 'Epic', 'Once per life, if you take damage that would reduce you below 30% Max Life, first gain 300 decaying Overhealth.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (72, 'Gloomgauntlet', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '10000.0', 'Epic', '[Melee] damage grants 10% Move Speed and restores 5% of Max Life over 2s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (73, 'Martian Mender', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '10000.0', 'Epic', 'Restore 3% of your Life every 1s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (74, 'Phantasmic Flux', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '10000.0', 'Epic', 'While at full Life, Lifesteal grants up to 100 Overhealth.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (75, 'Rüstung von Wilhelm', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '10000.0', 'Epic', 'While below 30% Life, gain 10% Damage Reduction.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (76, 'Vanadium Injection', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '10000.0', 'Epic', 'While at 100% Ultimate Charge, gain:
 50 Health
 10% Weapon Power
 10% Ability Power
 10% Attack Speed
 10% Cooldown Reduction
 10% Move Speed');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (77, 'Nebula Conduit', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '11000.0', 'Epic', 'Prevent 15% of incoming damage and instead take that prevented damage over 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (78, 'Ogundimu Reduction Field', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '11000.0', 'Epic', 'When you take damage, gain 0.5% damage reduction for 1s, stacking up to 20 times.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (79, 'Aura Repellant
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '1500.0', 'Common', 'On Use: Knockback all enemies within 8m.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (80, 'Barrier Snippet
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '1500.0', 'Common', 'On Use: Gain 50 Overhealth for 3s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (81, 'Exo Springs
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '1500.0', 'Common', 'Holding [Crouch] for up to 0.75 seconds increases the height of your next jump by up to 250%.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (82, 'Feathered Soles
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '1500.0', 'Common', 'On Use: Gain 25% Move Speed for 5s. Dealing damage removes this bonus.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (83, 'Med Kit
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '1500.0', 'Common', 'On Use: Restore 20 Life every 1s for 10s. Taking damage removes this bonus.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (84, 'Dashing Skates
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '3750.0', 'Rare', 'On Use: Dash a short distance.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (85, 'Field Support
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '4000.0', 'Rare', 'On Use: Place a Biotic Field at your location that restores 50 Life every 1s for 5s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (86, 'Kitsune Snippet
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '4000.0', 'Rare', 'On Use: Gain 50 Overhealth and 25% Move Speed for 3s. If you are debuffed, gain an additional 100 Overhealth.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (87, 'Kitsune Charm
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '9000.0', 'Epic', 'On Use: Cleanse yourself of most negative effects, restore 75 Life, and gain Invulnerability and 75% Move Speed for 0.65s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (88, 'Jet Skates
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '9500.0', 'Epic', 'On Use: Dash a short distance.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (89, 'Necrotic Repellant
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '9000.0', 'Epic', 'On Use: Knockback, Hinder and slow all enemies within 4m for 1s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (90, 'Super Serum
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '10000.0', 'Epic', 'On Use: Increase your total Attack Speed by 50% but deal 15% reduced Weapon Damage and Healing for 5s. Reload all your Ammo.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (91, 'Colossus Core
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '9500.0', 'Epic', 'On Use: Gain Unstoppable, 25% Damage Reduction, 25% Max Life, and grow in size, but deal 25% reduced damage and healing for 5s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (92, 'Refresher
3', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'Gadget', '13000.0', 'Epic', 'Reduce all Ability cooldowns by 8s.');
INSERT INTO "Items" ("Id", "Name", "ImageUri", "Type", "Cost", "Rarity", "Description") VALUES (93, 'Heartbeat Sensor', 'data:image/gif;base64,R0lGODlhAQABAIABAAAAAP///yH5BAEAAAEALAAAAAABAAEAQAICTAEAOw%3D%3D', 'General Item (Survival)', '1500.0', 'Common', 'Enemies below 30% Life are Revealed to you.');

-- Table: HeroExclusives

-- Table: Buffs
INSERT INTO "Buffs" ("Id", "Name") VALUES (1, 'Weapon Power');
INSERT INTO "Buffs" ("Id", "Name") VALUES (2, 'Weapon Lifesteal');
INSERT INTO "Buffs" ("Id", "Name") VALUES (3, 'Attack Speed');
INSERT INTO "Buffs" ("Id", "Name") VALUES (4, 'Max Ammo');
INSERT INTO "Buffs" ("Id", "Name") VALUES (5, 'Health');
INSERT INTO "Buffs" ("Id", "Name") VALUES (6, 'Move Speed');
INSERT INTO "Buffs" ("Id", "Name") VALUES (7, 'Shields');
INSERT INTO "Buffs" ("Id", "Name") VALUES (8, 'Cooldown Reduction');
INSERT INTO "Buffs" ("Id", "Name") VALUES (9, 'Increased Damage to Barriers and Deployables');
INSERT INTO "Buffs" ("Id", "Name") VALUES (10, 'Armor');
INSERT INTO "Buffs" ("Id", "Name") VALUES (11, 'Critical Damage');
INSERT INTO "Buffs" ("Id", "Name") VALUES (12, 'Ability Power');
INSERT INTO "Buffs" ("Id", "Name") VALUES (13, 'Ability Lifesteal');
INSERT INTO "Buffs" ("Id", "Name") VALUES (14, 'Starting Ultimate Charge');
INSERT INTO "Buffs" ("Id", "Name") VALUES (15, 'Negative Effect Duration');
INSERT INTO "Buffs" ("Id", "Name") VALUES (16, 'Health, Armor, Shields');
INSERT INTO "Buffs" ("Id", "Name") VALUES (17, 'Melee Damage');
INSERT INTO "Buffs" ("Id", "Name") VALUES (18, 'Incoming Negative Effect Duration');
INSERT INTO "Buffs" ("Id", "Name") VALUES (19, 'Knockback Resist');
INSERT INTO "Buffs" ("Id", "Name") VALUES (20, 'Slow Resist');

-- Table: ItemBuffs
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (1, 1, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (2, 2, 2, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (3, 3, 3, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (4, 4, 4, '20.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (5, 6, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (6, 7, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (7, 7, 6, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (8, 8, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (9, 9, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (10, 10, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (11, 10, 3, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (12, 10, 4, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (13, 11, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (14, 11, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (15, 12, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (16, 12, 3, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (17, 12, 2, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (18, 13, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (19, 13, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (20, 14, 1, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (21, 15, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (22, 15, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (23, 16, 1, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (24, 17, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (25, 17, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (26, 17, 9, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (27, 18, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (28, 19, 10, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (29, 19, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (30, 20, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (31, 20, 1, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (32, 21, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (33, 21, 4, '40.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (34, 22, 3, '20.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (35, 23, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (36, 24, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (37, 24, 8, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (38, 25, 1, '20.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (39, 25, 11, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (40, 26, 1, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (41, 28, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (42, 29, 13, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (43, 30, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (44, 31, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (45, 31, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (46, 31, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (47, 32, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (48, 32, 12, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (49, 33, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (50, 33, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (51, 33, 13, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (52, 34, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (53, 34, 14, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (54, 35, 13, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (55, 36, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (56, 36, 12, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (57, 36, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (58, 37, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (59, 37, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (60, 38, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (61, 39, 12, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (62, 40, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (63, 40, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (64, 41, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (65, 41, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (66, 42, 12, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (67, 43, 10, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (68, 43, 12, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (69, 44, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (70, 44, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (71, 45, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (72, 45, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (73, 46, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (74, 46, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (75, 47, 12, '20.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (76, 47, 8, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (77, 48, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (78, 48, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (79, 49, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (80, 50, 12, '35.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (81, 51, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (82, 52, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (83, 53, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (84, 54, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (85, 55, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (86, 56, 10, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (87, 57, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (88, 58, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (89, 59, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (90, 60, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (91, 60, 15, '-40.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (92, 61, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (93, 61, 6, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (94, 62, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (95, 63, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (96, 64, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (97, 65, 5, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (98, 65, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (99, 66, 10, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (100, 67, 10, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (101, 68, 16, '8.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (102, 69, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (103, 70, 7, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (104, 71, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (105, 71, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (106, 72, 10, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (107, 72, 17, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (108, 73, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (109, 73, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (110, 74, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (111, 74, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (112, 74, 2, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (113, 74, 13, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (114, 75, 16, '15.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (115, 76, 7, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (116, 77, 5, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (117, 77, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (118, 78, 10, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (119, 79, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (120, 80, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (121, 81, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (122, 82, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (123, 83, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (124, 84, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (125, 84, 6, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (126, 85, 10, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (127, 85, 1, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (128, 86, 7, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (129, 86, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (130, 87, 7, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (131, 87, 18, '-40.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (132, 88, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (133, 88, 1, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (134, 88, 12, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (135, 89, 10, '50.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (136, 89, 8, '5.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (137, 90, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (138, 90, 3, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (139, 90, 2, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (140, 91, 16, '10.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (141, 91, 19, '40.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (142, 91, 20, '40.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (143, 92, 5, '25.0');
INSERT INTO "ItemBuffs" ("Id", "ItemId", "BuffId", "Value") VALUES (144, 93, 6, '5.0');

-- SQL Server: Disable Identity Insert
/*
SET IDENTITY_INSERT "Heroes" OFF;
SET IDENTITY_INSERT "Items" OFF;
SET IDENTITY_INSERT "HeroExclusives" OFF;
SET IDENTITY_INSERT "Buffs" OFF;
SET IDENTITY_INSERT "ItemBuffs" OFF;
*/

-- MySQL: Restore SQL Mode
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
