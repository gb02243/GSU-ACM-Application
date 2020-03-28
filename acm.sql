use acm_db;
CREATE TABLE `admin`  (
  `adminID` int NOT NULL AUTO_INCREMENT,
  `userID` int(0) NOT NULL,
  PRIMARY KEY (`adminID`)
);

CREATE TABLE `course`  (
  `courseID` int NOT NULL AUTO_INCREMENT,
  `courseCode` varchar(255) NULL,
  `courseTitle` varchar(255) NULL,
  `tutorID` int NOT NULL,
  PRIMARY KEY (`courseID`)
);

CREATE TABLE `message`  (
  `messageID` int NOT NULL AUTO_INCREMENT,
  `date` datetime NULL,
  `messageDescription` varchar(255) NULL,
  `senderID` int NULL,
  `recieverID` int NULL,
  PRIMARY KEY (`messageID`)
);

CREATE TABLE `reciever`  (
  `recieverID` int NOT NULL AUTO_INCREMENT,
  `userID` int NULL,
  PRIMARY KEY (`recieverID`)
);

CREATE TABLE `sender`  (
  `senderID` int NOT NULL AUTO_INCREMENT,
  `userID` int NULL,
  PRIMARY KEY (`senderID`)
);

CREATE TABLE `tutor`  (
  `tutorID` int NOT NULL AUTO_INCREMENT,
  `feedback` varchar(255) NULL,
  `userID` int NOT NULL ,
  PRIMARY KEY (`tutorID`)
);

CREATE TABLE `user`  (
  `userID` int NOT NULL AUTO_INCREMENT,
  `fname` varchar(255) NULL,
  `lname` varchar(255) NULL,
  `password` varchar(255) NULL,
  `email` varchar(255) NULL,
  `phone#` varchar(255) NULL,
  `currentClasses` varchar(255) NULL,
  `classesTaken` varchar(255) NULL,
  `isTutor` longblob NULL,
  `isAdmin` longblob NULL,
  `isBoardMember` longblob NULL,
  `boardTitle` varchar(255) NULL,
  `points` int(255) NULL,
  PRIMARY KEY (`userID`)
);

ALTER TABLE `admin` ADD CONSTRAINT `userFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `course` ADD CONSTRAINT `tutorFK` FOREIGN KEY (`tutorID`) REFERENCES `tutor` (`tutorID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `message` ADD CONSTRAINT `senderFK` FOREIGN KEY (`senderID`) REFERENCES `sender` (`senderID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `message` ADD CONSTRAINT `recieverFK` FOREIGN KEY (`recieverID`) REFERENCES `reciever` (`recieverID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `reciever` ADD CONSTRAINT `userRecieveFK` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `sender` ADD CONSTRAINT `userSendKey` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE `tutor` ADD CONSTRAINT `userTutorID` FOREIGN KEY (`userID`) REFERENCES `user` (`userID`) ON DELETE CASCADE ON UPDATE CASCADE;

insert
select *  from reciever;