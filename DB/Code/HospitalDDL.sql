-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema HospitalDB
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `HospitalDB` ;

-- -----------------------------------------------------
-- Schema HospitalDB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `HospitalDB` DEFAULT CHARACTER SET utf8 ;
USE `HospitalDB` ;

-- -----------------------------------------------------
-- Table `HospitalDB`.`Ala`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Ala` (
  `idAla` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idAla`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Zona`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Zona` (
  `idZona` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `lleno` TINYINT NOT NULL,
  `idAla` INT NOT NULL,
  PRIMARY KEY (`idZona`),
  INDEX `idAla_idx` (`idAla` ASC) VISIBLE,
  CONSTRAINT `idAla`
    FOREIGN KEY (`idAla`)
    REFERENCES `HospitalDB`.`Ala` (`idAla`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Paciente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Paciente` (
  `idPaciente` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `numTelefono` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `idZona` INT NOT NULL,
  PRIMARY KEY (`idPaciente`),
  INDEX `fk_Paciente_Zona1_idx` (`idZona` ASC) VISIBLE,
  CONSTRAINT `fk_Paciente_Zona1`
    FOREIGN KEY (`idZona`)
    REFERENCES `HospitalDB`.`Zona` (`idZona`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Enfermero`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Enfermero` (
  `idEnfermero` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NULL,
  `numTelefono` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `estado` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idEnfermero`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Alarma`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Alarma` (
  `idAlarma` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `idZona` INT NOT NULL,
  PRIMARY KEY (`idAlarma`),
  INDEX `idZona_idx` (`idZona` ASC) VISIBLE,
  CONSTRAINT `idZona`
    FOREIGN KEY (`idZona`)
    REFERENCES `HospitalDB`.`Zona` (`idZona`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`PacientesXEnfermero`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`PacientesXEnfermero` (
  `idRegistro` INT NOT NULL,
  `idEnfermero` INT NOT NULL,
  `idPaciente` INT NOT NULL,
  PRIMARY KEY (`idRegistro`),
  INDEX `idEnfermero_idx` (`idEnfermero` ASC) VISIBLE,
  INDEX `idPaciente_idx` (`idPaciente` ASC) VISIBLE,
  CONSTRAINT `idEnfermero`
    FOREIGN KEY (`idEnfermero`)
    REFERENCES `HospitalDB`.`Enfermero` (`idEnfermero`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `idPaciente`
    FOREIGN KEY (`idPaciente`)
    REFERENCES `HospitalDB`.`Paciente` (`idPaciente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Boton`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Boton` (
  `idBoton` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `idZona` INT NOT NULL,
  PRIMARY KEY (`idBoton`),
  INDEX `fk_Boton_Zona1_idx` (`idZona` ASC) VISIBLE,
  CONSTRAINT `fk_Boton_Zona1`
    FOREIGN KEY (`idZona`)
    REFERENCES `HospitalDB`.`Zona` (`idZona`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`ActivacionAlarma`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`ActivacionAlarma` (
  `idActivacionAlarma` INT NOT NULL AUTO_INCREMENT,
  `codigo` VARCHAR(45) NOT NULL,
  `idZona` INT NOT NULL,
  `idBoton` INT NOT NULL,
  `fechaActivacion` DATETIME(3) NOT NULL,
  PRIMARY KEY (`idActivacionAlarma`),
  INDEX `idBoton_idx` (`idBoton` ASC) VISIBLE,
  INDEX `fk_ActivacionAlarma_Zona1_idx` (`idZona` ASC) VISIBLE,
  CONSTRAINT `idBoton`
    FOREIGN KEY (`idBoton`)
    REFERENCES `HospitalDB`.`Boton` (`idBoton`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ActivacionAlarma_Zona1`
    FOREIGN KEY (`idZona`)
    REFERENCES `HospitalDB`.`Zona` (`idZona`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Usuario` (
  `idUsuario` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `tipo` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NULL,
  `estado` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUsuario`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`Permiso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`Permiso` (
  `idPermiso` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idPermiso`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `HospitalDB`.`PermisosXUsuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `HospitalDB`.`PermisosXUsuario` (
  `idRegistro` INT NOT NULL AUTO_INCREMENT,
  `idUsuario` INT NOT NULL,
  `idPermiso` INT NOT NULL,
  PRIMARY KEY (`idRegistro`),
  INDEX `fk_PermisosXUsuario_Usuario1_idx` (`idUsuario` ASC) VISIBLE,
  INDEX `fk_PermisosXUsuario_Permiso1_idx` (`idPermiso` ASC) VISIBLE,
  CONSTRAINT `fk_PermisosXUsuario_Usuario1`
    FOREIGN KEY (`idUsuario`)
    REFERENCES `HospitalDB`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_PermisosXUsuario_Permiso1`
    FOREIGN KEY (`idPermiso`)
    REFERENCES `HospitalDB`.`Permiso` (`idPermiso`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
