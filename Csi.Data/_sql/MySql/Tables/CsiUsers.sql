CREATE TABLE `CsiUsers` (
	`Id` VARCHAR(767) NOT NULL COLLATE 'utf8mb4_unicode_ci',
	`UserName` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`NormalizedUserName` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`Email` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`NormalizedEmail` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`EmailConfirmed` BIT(1) NOT NULL,
	`PasswordHash` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`SecurityStamp` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`ConcurrencyStamp` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`PhoneNumber` TEXT NULL COLLATE 'utf8mb4_unicode_ci',
	`PhoneNumberConfirmed` BIT(1) NOT NULL,
	`TwoFactorEnabled` BIT(1) NOT NULL,
	`LockoutEnd` TIMESTAMP NULL DEFAULT NULL,
	`LockoutEnabled` BIT(1) NOT NULL,
	`AccessFailedCount` INT(11) NOT NULL,
	PRIMARY KEY (`Id`)
)
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;
