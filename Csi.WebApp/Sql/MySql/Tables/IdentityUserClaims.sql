CREATE TABLE `IdentityUserClaims` (
	`Id` INT(11) NOT NULL AUTO_INCREMENT,
	`UserId` TEXT NULL,
	`ClaimType` TEXT NULL,
	`ClaimValue` TEXT NULL,
	PRIMARY KEY (`Id`)
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
;
