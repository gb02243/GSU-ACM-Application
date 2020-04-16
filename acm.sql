DROP DATABASE IF EXISTS GSUACM;
CREATE DATABASE GSUACM;
USE GSUACM;
--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin` (
  `adminID` int(11) NOT NULL AUTO_INCREMENT,
  `userID` int(11) NOT NULL,
  PRIMARY KEY (`adminID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `course`
--

DROP TABLE IF EXISTS `course`;
CREATE TABLE `course` (
  `courseID` int(11) NOT NULL AUTO_INCREMENT,
  `courseCode` varchar(255) DEFAULT NULL,
  `courseTitle` varchar(255) DEFAULT NULL,
  `tutorID` int(11) NOT NULL,
  PRIMARY KEY (`courseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `message`
--

DROP TABLE IF EXISTS `message`;
CREATE TABLE `message` (
  `messageID` varchar(36) NOT NULL,
  `date` varchar(255) DEFAULT NULL,
  `messageDescription` varchar(255) DEFAULT NULL,
  `senderID` int(11) DEFAULT NULL,
  `recieverID` int(11) DEFAULT NULL,
  PRIMARY KEY (`messageID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `newsitem`
--

DROP TABLE IF EXISTS `newsitem`;
CREATE TABLE `newsitem` (
  `newsitemID` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT NULL,
  `author` varchar(255) DEFAULT NULL,
  `date` varchar(255) DEFAULT NULL,
  `time` timestamp NULL DEFAULT NULL,
  `body` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`newsitemID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `options`
--

DROP TABLE IF EXISTS `option`;
CREATE TABLE `option` (
  `optionID` varchar(36) NOT NULL,
  `pollID` varchar(36) NOT NULL,
  `text` varchar(255) DEFAULT NULL,
  `votes` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`optionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `poll`
--

DROP TABLE IF EXISTS `poll`;
CREATE TABLE `poll` (
  `pollID` varchar(36) NOT NULL,
  `pollAuthorID` int(11) NOT NULL,
  `title` varchar(255) DEFAULT NULL,
  `date` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`pollID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `reciever`
--

DROP TABLE IF EXISTS `reciever`;
CREATE TABLE `reciever` (
  `recieverID` int(11) NOT NULL,
  `userID` int(11) DEFAULT NULL,
  PRIMARY KEY (`recieverID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
--
-- Table structure for table `sender`
--

DROP TABLE IF EXISTS `sender`;
CREATE TABLE `sender` (
  `senderID` int(11) NOT NULL,
  `userID` int(11) DEFAULT NULL,
  PRIMARY KEY (`senderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `tutor`
--

DROP TABLE IF EXISTS `tutor`;
CREATE TABLE `tutor` (
  `tutorID` int(11) NOT NULL AUTO_INCREMENT,
  `feedback` varchar(255) DEFAULT NULL,
  `userID` int(11) NOT NULL,
  PRIMARY KEY (`tutorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `tutorsession`
--

DROP TABLE IF EXISTS `tutorsession`;
CREATE TABLE `tutorsession` (
  `sessionID` int(11) NOT NULL AUTO_INCREMENT,
  `subject` varchar(255) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `tutorID` int(11) DEFAULT NULL,
  `userID` int(11) DEFAULT NULL,
  PRIMARY KEY (`sessionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `userID` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(255) DEFAULT NULL,
  `lname` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phone` varchar(255) DEFAULT NULL,
  `currentClasses` varchar(255) DEFAULT NULL,
  `classesTaken` varchar(255) DEFAULT NULL,
  `isTutor` longblob,
  `isAdmin` longblob,
  `isBoardMember` longblob,
  `title` varchar(255) DEFAULT NULL,
  `points` int(255) DEFAULT NULL,
  PRIMARY KEY (`userID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

ALTER TABLE `admin` ADD CONSTRAINT `userFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `course` ADD CONSTRAINT `tutorFK` FOREIGN KEY (`tutorID`) REFERENCES `tutor` (`tutorID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `message` ADD CONSTRAINT `messageSenderFK` FOREIGN KEY (`senderID`) REFERENCES `sender` (`senderID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `message` ADD CONSTRAINT `messageRecieverFK` FOREIGN KEY (`recieverID`) REFERENCES `reciever` (`recieverID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `reciever` ADD CONSTRAINT `userRecieveFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `sender` ADD CONSTRAINT `userSendKey` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `tutor` ADD CONSTRAINT `userTutorID` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `option` ADD CONSTRAINT `pollFK` FOREIGN KEY (`pollID`) REFERENCES `poll` (`pollID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `poll` ADD CONSTRAINT `authorFK` FOREIGN KEY (`pollAuthorID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `reciever` ADD CONSTRAINT `recieverFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `sender` ADD CONSTRAINT `senderFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `tutor` ADD CONSTRAINT `tutorUserFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `tutorsession` ADD CONSTRAINT `sessionTutorFK` FOREIGN KEY (`tutorID`) REFERENCES `tutor` (`tutorID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `tutorsession` ADD CONSTRAINT `sessionUserFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;

INSERT INTO user(userID, fname, lname, password, email, title) VALUES('1', 'Griffin', 'Bryant', 'test', 'test@gmail.com', 'Administrator');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('2', 'Test1', 'Test1', 'test', 'test1@gmail.com', 'Member');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('3', 'Test2', 'Test2', 'test', 'test2@gmail.com', 'Member');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('4', 'Test3', 'Test3', 'test', 'test3@gmail.com', 'Member');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('5', 'Test4', 'Test4', 'test', 'test4@gmail.com', 'Member');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('6', 'Test5', 'Test5', 'test', 'test5@gmail.com', 'Mentor');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('7', 'Test6', 'Test6', 'test', 'test6@gmail.com', 'Mentor');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('8', 'Test7', 'Test7', 'test', 'test7@gmail.com', 'Member');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('9', 'Test8', 'Test8', 'test', 'test8@gmail.com', 'Member');
INSERT INTO user(userID, fname, lname, password, email, title) VALUES('10', 'Test9', 'Test9', 'test', 'test9@gmail.com', 'Mentor');