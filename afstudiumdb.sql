-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Erstellungszeit: 16. Jan 2025 um 11:58
-- Server-Version: 5.7.24
-- PHP-Version: 8.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `afstudiumdb`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `connectionstable`
--

CREATE TABLE `connectionstable` (
  `Id` int(11) NOT NULL,
  `StudentId` int(11) DEFAULT NULL,
  `EventId` int(11) NOT NULL,
  `Status` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `connectionstable`
--

INSERT INTO `connectionstable` (`Id`, `StudentId`, `EventId`, `Status`) VALUES
(65, 100000, 60, 1),
(66, 257518, 60, 3),
(70, 100000, 5, 1),
(71, 100000, 12, 1),
(72, 100000, 25, 1),
(73, 100000, 35, 1),
(74, 100000, 36, 1),
(75, 100000, 38, 1),
(76, 100000, 41, 1),
(77, 100000, 52, 1),
(78, 100000, 54, 1),
(80, 257517, 23, 1),
(81, 257517, 44, 1),
(82, 257517, 45, 1),
(85, 259068, 23, 3),
(86, 259068, 36, 3),
(87, 259068, 45, 3),
(88, 259068, 52, 3),
(89, 257518, 54, 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `eventstable`
--

CREATE TABLE `eventstable` (
  `EventId` int(16) NOT NULL,
  `SubjectId` int(16) NOT NULL,
  `EventName` varchar(128) NOT NULL,
  `EventType` varchar(64) NOT NULL,
  `StudentsAmount` int(16) NOT NULL,
  `createdperson` int(6) NOT NULL,
  `date` varchar(64) NOT NULL,
  `time` varchar(64) NOT NULL,
  `Credits` int(8) NOT NULL,
  `Location` varchar(128) NOT NULL,
  `PermitRequired` tinyint(1) NOT NULL,
  `PermitionEvent` int(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `eventstable`
--

INSERT INTO `eventstable` (`EventId`, `SubjectId`, `EventName`, `EventType`, `StudentsAmount`, `createdperson`, `date`, `time`, `Credits`, `Location`, `PermitRequired`, `PermitionEvent`) VALUES
(5, 6, 'AnChe Übung Gruppe 17', 'Übung', 0, 100000, '01.01.2025', '14:00-16:00', 0, '', 0, 0),
(12, 6, 'Anorganische Chemie Tutorium', 'Tutorium', 0, 100000, 'Montag', '14:00-16:00', 0, '', 0, 0),
(23, 7, 'Lineare Algebra I Vorlesung', 'Vorlesung', 1, 257517, 'Montag Dienstag Mittwoch Donnerstag Freitag', '14:00-16:00', 0, 'HG II HS 1', 0, 0),
(25, 6, 'Anorganische Chemie Vorlesung', 'Vorlesung', 0, 100000, '01.01.2025', '14:00:00-16:00:00', 0, '', 0, 0),
(35, 7, 'Lineare Algebra I Seminar', 'Seminar', 0, 100000, '', '01:00:00-05:00:00', 0, 'srg', 0, 0),
(36, 7, 'Lineare Algebra I Übung', 'Übung', 1, 100000, 'Montag Dienstag', '12:00:00-14:00:00', 3, 'bambam', 0, 0),
(38, 8, 'DAP 1 Klausur', 'Klausur', 0, 100000, '01.03.2025', '01:00:00-04:00:00', 10, 'SRG', 1, 41),
(41, 8, 'Studienleistung für DAP 1 Klausur', 'Studienleistung', 0, 100000, '', '', 0, '', 0, 0),
(44, 7, 'Studienleistung für Lineare Algebra I Klausur', 'Studienleistung', 0, 257517, '', '', 0, '', 0, 0),
(45, 7, 'Lineare Algebra I Klausur', 'Klausur', 1, 257517, '31.01.2025', '10:00-12:00', 0, 'tam', 1, 44),
(52, 8, 'DAP 1 Seminar', 'Seminar', 1, 100000, 'Montag Dienstag Mittwoch Donnerstag Freitag', '00:00-00:00', 0, 'ab', 0, 0),
(54, 6, 'Anorganische Chemie Klausur', 'Klausur', 1, 100000, 'Montag Dienstag Mittwoch Donnerstag Freitag', '15:00-17:00', 6, 'tam', 1, 60),
(60, 6, 'Studienleistung für Anorganische Chemie Klausur', 'Studienleistung', 1, 100000, '', '', 0, '', 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gradestable`
--

CREATE TABLE `gradestable` (
  `Id` int(11) NOT NULL,
  `EventId` int(11) NOT NULL,
  `StudentId` int(11) NOT NULL,
  `Grade` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `gradestable`
--

INSERT INTO `gradestable` (`Id`, `EventId`, `StudentId`, `Grade`) VALUES
(2, 45, 257517, '2.7'),
(3, 45, 333333, '1.7'),
(6, 38, 333333, '4.0'),
(7, 45, 259068, ''),
(8, 45, 259068, ''),
(9, 54, 257518, '');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `messagestable`
--

CREATE TABLE `messagestable` (
  `MessageId` int(11) NOT NULL,
  `EventId` int(16) NOT NULL,
  `SendFrom` int(6) NOT NULL,
  `SendTo` int(6) NOT NULL,
  `MessageHeader` varchar(256) DEFAULT NULL,
  `MessageText` varchar(1024) DEFAULT NULL,
  `MessageTime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `statustable`
--

CREATE TABLE `statustable` (
  `StatusId` int(11) NOT NULL,
  `Status` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `statustable`
--

INSERT INTO `statustable` (`StatusId`, `Status`) VALUES
(1, 'creator'),
(2, 'assistent'),
(3, 'student');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `subjectstable`
--

CREATE TABLE `subjectstable` (
  `subjectid` int(16) NOT NULL,
  `subjectname` varchar(64) NOT NULL,
  `faculty` varchar(64) NOT NULL,
  `CreatedPerson` int(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `subjectstable`
--

INSERT INTO `subjectstable` (`subjectid`, `subjectname`, `faculty`, `CreatedPerson`) VALUES
(6, 'Anorganische Chemie', 'Fakultät BCI', 100000),
(7, 'Lineare Algebra I', 'Fakultät Mathematik', 257517),
(8, 'DAP 1', 'Fakultät Informatik', 100000);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `userstable`
--

CREATE TABLE `userstable` (
  `MatrikelNum` int(6) NOT NULL,
  `Email` varchar(64) NOT NULL,
  `Password` varchar(64) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Surname` varchar(64) NOT NULL,
  `Course` varchar(64) NOT NULL,
  `Semester` int(3) DEFAULT NULL,
  `Role` varchar(16) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `userstable`
--

INSERT INTO `userstable` (`MatrikelNum`, `Email`, `Password`, `Name`, `Surname`, `Course`, `Semester`, `Role`) VALUES
(2, 'stringe', 'string[', 'stringn', 'strings', '', 0, 'admin'),
(100000, 'testemail', 'testpass', 't', 'e', '', 0, 'teacher'),
(257517, 'a', 'b', 'c', 'd', '', 0, 'teacher'),
(257518, 'ag', 'b', 'c', 'student', 'AngInf', 3, 'student'),
(259068, 'lm', 'chuki12', 'liza', 'metonidze', 'Wirtschaftsmathematik', 1, 'student'),
(333333, 'ah', 'b', 'c', 'student2', 'e', 99, 'student');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `connectionstable`
--
ALTER TABLE `connectionstable`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `StudentId` (`StudentId`),
  ADD KEY `EventId` (`EventId`) USING BTREE,
  ADD KEY `Status` (`Status`);

--
-- Indizes für die Tabelle `eventstable`
--
ALTER TABLE `eventstable`
  ADD PRIMARY KEY (`EventId`),
  ADD UNIQUE KEY `SubjectId` (`SubjectId`,`EventName`),
  ADD KEY `createdperson` (`createdperson`);

--
-- Indizes für die Tabelle `gradestable`
--
ALTER TABLE `gradestable`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `EventId` (`EventId`),
  ADD KEY `StudentId` (`StudentId`);

--
-- Indizes für die Tabelle `messagestable`
--
ALTER TABLE `messagestable`
  ADD PRIMARY KEY (`MessageId`),
  ADD UNIQUE KEY `EventId` (`EventId`),
  ADD KEY `SendFrom` (`SendFrom`),
  ADD KEY `SendTo` (`SendTo`);

--
-- Indizes für die Tabelle `statustable`
--
ALTER TABLE `statustable`
  ADD PRIMARY KEY (`StatusId`);

--
-- Indizes für die Tabelle `subjectstable`
--
ALTER TABLE `subjectstable`
  ADD PRIMARY KEY (`subjectid`),
  ADD UNIQUE KEY `subjectid` (`subjectid`);

--
-- Indizes für die Tabelle `userstable`
--
ALTER TABLE `userstable`
  ADD PRIMARY KEY (`MatrikelNum`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `connectionstable`
--
ALTER TABLE `connectionstable`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=90;

--
-- AUTO_INCREMENT für Tabelle `eventstable`
--
ALTER TABLE `eventstable`
  MODIFY `EventId` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT für Tabelle `gradestable`
--
ALTER TABLE `gradestable`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT für Tabelle `messagestable`
--
ALTER TABLE `messagestable`
  MODIFY `MessageId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `statustable`
--
ALTER TABLE `statustable`
  MODIFY `StatusId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `subjectstable`
--
ALTER TABLE `subjectstable`
  MODIFY `subjectid` int(16) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `connectionstable`
--
ALTER TABLE `connectionstable`
  ADD CONSTRAINT `blabla` FOREIGN KEY (`EventId`) REFERENCES `eventstable` (`EventId`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `connectionstable_ibfk_1` FOREIGN KEY (`StudentId`) REFERENCES `userstable` (`MatrikelNum`),
  ADD CONSTRAINT `statustostatusid` FOREIGN KEY (`Status`) REFERENCES `statustable` (`StatusId`);

--
-- Constraints der Tabelle `eventstable`
--
ALTER TABLE `eventstable`
  ADD CONSTRAINT `eventstable_ibfk_1` FOREIGN KEY (`SubjectId`) REFERENCES `subjectstable` (`subjectid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints der Tabelle `gradestable`
--
ALTER TABLE `gradestable`
  ADD CONSTRAINT `StudentIdToStudentId` FOREIGN KEY (`StudentId`) REFERENCES `userstable` (`MatrikelNum`),
  ADD CONSTRAINT `eventidtoeventid` FOREIGN KEY (`EventId`) REFERENCES `eventstable` (`EventId`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints der Tabelle `messagestable`
--
ALTER TABLE `messagestable`
  ADD CONSTRAINT `messagestable_ibfk_1` FOREIGN KEY (`SendFrom`) REFERENCES `userstable` (`MatrikelNum`),
  ADD CONSTRAINT `messagestable_ibfk_2` FOREIGN KEY (`SendTo`) REFERENCES `userstable` (`MatrikelNum`),
  ADD CONSTRAINT `messagestable_ibfk_3` FOREIGN KEY (`EventId`) REFERENCES `eventstable` (`EventId`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
