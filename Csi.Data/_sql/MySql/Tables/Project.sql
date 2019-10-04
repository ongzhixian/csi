CREATE TABLE `Project` (
	`id` VARCHAR(36) NOT NULL COLLATE 'utf8mb4_unicode_ci',
	`name` VARCHAR(50) NOT NULL COLLATE 'utf8mb4_unicode_ci',
	`start_date` DATE NULL DEFAULT NULL,
	`end_date` DATE NULL DEFAULT NULL,
	`status` TINYINT(3) UNSIGNED NOT NULL DEFAULT '0',
	`cre_by` VARCHAR(36) NOT NULL COLLATE 'utf8mb4_unicode_ci',
	`cre_dt` DATETIME NOT NULL,
	`upd_by` VARCHAR(36) NULL DEFAULT NULL COLLATE 'utf8mb4_unicode_ci',
	`upd_dt` DATETIME NULL DEFAULT NULL,
	`del_by` VARCHAR(36) NULL DEFAULT NULL COLLATE 'utf8mb4_unicode_ci',
	`del_dt` DATETIME NULL DEFAULT NULL,
	PRIMARY KEY (`id`)
)
COLLATE='utf8mb4_unicode_ci'
ENGINE=InnoDB
;
