CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'PK',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: Account Id',
  name VARCHAR(255) COMMENT 'Keep Name',
  description VARCHAR(255) COMMENT 'Keep description',
  img VARCHAR(255) COMMENT 'Keep Img',
  isDeleted TINYINT NOT NULL DEFAULT 0,
  views int NOT NULL DEFAULT 0,
  shares int NOT NULL DEFAULT 0,
  keeps int NOT NULL DEFAULT 0,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE keeps;

CREATE TABLE IF NOT EXISTS vaults(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'PK',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: Account Id',
  name VARCHAR(255) COMMENT 'Keep Name',
  description VARCHAR(255) COMMENT 'Keep description',
  isPrivate TINYINT DEFAULT 0,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE vaults;

CREATE TABLE IF NOT EXISTS vault_keeps(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'PK',
  vaultId int NOT NULL COMMENT 'FK: Vault Id',
  keepId int NOT NULL COMMENT 'FK: Keep Id',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: Account Id',
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET utf8 COMMENT '';

DROP TABLE vault_keeps;

 SELECT 
            vk.id as vaultKeepId,
            vk.vaultId as vaultId,
            vk.keepId as keepId,
            v.*,
            k.*,
            a.*
            FROM vault_keeps vk
            JOIN vaults v ON v.id = vk.vaultId
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a on a.id = vk.creatorId
            WHERE vk.vaultId = 28;