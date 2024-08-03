-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Mar 24, 2023 at 02:07 AM
-- Server version: 8.0.30
-- PHP Version: 8.1.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `fast_typing_app_db_2`
--

-- --------------------------------------------------------

--
-- Table structure for table `Exercises`
--

CREATE TABLE `Exercises` (
  `Id` int NOT NULL,
  `Name` text NOT NULL,
  `Level` int NOT NULL,
  `ErrorNum` int NOT NULL,
  `Text` text NOT NULL,
  `MaxTime` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Exercises`
--

INSERT INTO `Exercises` (`Id`, `Name`, `Level`, `ErrorNum`, `Text`, `MaxTime`) VALUES
(8, 'Упражение 1', 1, 1, '123456789', 5),
(9, 'Упражнение 2', 1, 1, '123', 10),
(10, 'рус', 2, 1, 'йццкцккцкцваывывп\"24', 20),
(11, 'eng', 1, 1, 'qwrwrwrqwqw', 20);

-- --------------------------------------------------------

--
-- Table structure for table `UserExercises`
--

CREATE TABLE `UserExercises` (
  `Id` int NOT NULL,
  `UserId` int NOT NULL,
  `ExerciseId` int NOT NULL,
  `ErrorNum` int NOT NULL,
  `Accuracy` double NOT NULL,
  `Time` double NOT NULL,
  `Succefull` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `UserExercises`
--

INSERT INTO `UserExercises` (`Id`, `UserId`, `ExerciseId`, `ErrorNum`, `Accuracy`, `Time`, `Succefull`) VALUES
(49, 1, 9, 0, 100, 0.57, 1),
(50, 1, 9, 3, 40, 5.6, 0),
(53, 1, 8, 0, 100, 3.58, 1),
(54, 1, 9, 0, 100, 0.72, 1),
(55, 1, 11, 3, 76.92307692307692, 7.05, 0),
(56, 1, 10, 6, 76, 28.02, 0),
(57, 2, 8, 0, 100, 2.63, 1);

-- --------------------------------------------------------

--
-- Table structure for table `Users`
--

CREATE TABLE `Users` (
  `Id` int NOT NULL,
  `Name` varchar(767) NOT NULL,
  `Password` text NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `Users`
--

INSERT INTO `Users` (`Id`, `Name`, `Password`, `IsAdmin`) VALUES
(1, 'admin', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 1),
(2, 'user1', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0),
(11, 'user2', '65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5', 0);

-- --------------------------------------------------------

--
-- Table structure for table `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20230321184747_Migration1', '5.0.17'),
('20230321205912_Migration2', '5.0.17'),
('20230322104436_Migration3', '5.0.17'),
('20230323154406_Migration4', '5.0.17'),
('20230323211926_Migration5', '5.0.17');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Exercises`
--
ALTER TABLE `Exercises`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `UserExercises`
--
ALTER TABLE `UserExercises`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_UserExercises_ExerciseId` (`ExerciseId`),
  ADD KEY `IX_UserExercises_UserId` (`UserId`);

--
-- Indexes for table `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Users_Name` (`Name`);

--
-- Indexes for table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Exercises`
--
ALTER TABLE `Exercises`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `UserExercises`
--
ALTER TABLE `UserExercises`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- AUTO_INCREMENT for table `Users`
--
ALTER TABLE `Users`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `UserExercises`
--
ALTER TABLE `UserExercises`
  ADD CONSTRAINT `FK_UserExercises_Exercises_ExerciseId` FOREIGN KEY (`ExerciseId`) REFERENCES `Exercises` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_UserExercises_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
