-- MySQL dump 10.13  Distrib 9.1.0, for Linux (x86_64)
--
-- Host: localhost    Database: kpsistema
-- ------------------------------------------------------
-- Server version	9.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Communities`
--

DROP TABLE IF EXISTS `Communities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Communities` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Communities`
--

LOCK TABLES `Communities` WRITE;
/*!40000 ALTER TABLE `Communities` DISABLE KEYS */;
INSERT INTO `Communities` VALUES (1,'Green Valley Community','12 Oak Street'),(2,'Sunrise Heights','45 Maple Avenue'),(3,'River Park Residences','78 River Road'),(4,'Lakeside Community','3 Lake View Blvd'),(5,'Hilltop Estates','99 Hilltop Lane'),(6,'Pinewood Community','21 Pine Street'),(7,'Meadowbrook Living','56 Meadow Drive'),(8,'Cedar Grove','14 Cedar Court'),(9,'Willow Creek','88 Willow Way'),(10,'Silverstone Community','101 Silver Street'),(11,'Oakridge Homes','7 Oakridge Road'),(12,'Harbor View Community','62 Harbor Lane'),(13,'Forest Edge','29 Forest Drive'),(14,'Parkside Living','5 Park Avenue'),(15,'Mountain View Community','150 Mountain Road');
/*!40000 ALTER TABLE `Communities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Roles`
--

DROP TABLE IF EXISTS `Roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Roles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Roles`
--

LOCK TABLES `Roles` WRITE;
/*!40000 ALTER TABLE `Roles` DISABLE KEYS */;
INSERT INTO `Roles` VALUES (1,'User'),(2,'Manager'),(3,'Admin');
/*!40000 ALTER TABLE `Roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CommunityId` int DEFAULT NULL,
  `RoleId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Users_CommunityId` (`CommunityId`),
  KEY `IX_Users_RoleId` (`RoleId`),
  CONSTRAINT `FK_Users_Communities_CommunityId` FOREIGN KEY (`CommunityId`) REFERENCES `Communities` (`Id`),
  CONSTRAINT `FK_Users_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UtilityTasks`
--

DROP TABLE IF EXISTS `UtilityTasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `UtilityTasks` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `CommunityId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_UtilityTasks_CommunityId` (`CommunityId`),
  CONSTRAINT `FK_UtilityTasks_Communities_CommunityId` FOREIGN KEY (`CommunityId`) REFERENCES `Communities` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UtilityTasks`
--

LOCK TABLES `UtilityTasks` WRITE;
/*!40000 ALTER TABLE `UtilityTasks` DISABLE KEYS */;
INSERT INTO `UtilityTasks` VALUES (1,'Task 1 for Green Valley Community','Demo task description',100.000000000000000000000000000000,1),(2,'Task 2 for Sunrise Heights','Demo task description',120.000000000000000000000000000000,2),(3,'Task 3 for River Park Residences','Demo task description',130.000000000000000000000000000000,3),(4,'Task 4 for Lakeside Community','Demo task description',140.000000000000000000000000000000,4),(5,'Task 5 for Hilltop Estates','Demo task description',150.000000000000000000000000000000,5),(6,'Task 6 for Pinewood Community','Demo task description',160.000000000000000000000000000000,6),(7,'Task 7 for Meadowbrook Living','Demo task description',170.000000000000000000000000000000,7),(8,'Task 8 for Cedar Grove','Demo task description',180.000000000000000000000000000000,8),(9,'Task 9 for Willow Creek','Demo task description',190.000000000000000000000000000000,9),(10,'Task 10 for Silverstone Community','Demo task description',200.000000000000000000000000000000,10),(11,'Task 11 for Oakridge Homes','Demo task description',210.000000000000000000000000000000,11),(12,'Task 12 for Harbor View Community','Demo task description',220.000000000000000000000000000000,12),(13,'Task 13 for Forest Edge','Demo task description',230.000000000000000000000000000000,13),(14,'Task 14 for Parkside Living','Demo task description',240.000000000000000000000000000000,14),(15,'Task 15 for Mountain View Community','Demo task description',250.000000000000000000000000000000,15);
/*!40000 ALTER TABLE `UtilityTasks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20251221131214_InitialCreate','9.0.10');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-12-21 13:28:08
