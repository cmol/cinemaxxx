-- phpMyAdmin SQL Dump
-- version 3.3.2deb1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Jun 10, 2011 at 09:45 AM
-- Server version: 5.1.41
-- PHP Version: 5.3.2-1ubuntu4.9

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `cinemaxxx`
--

-- --------------------------------------------------------

--
-- Table structure for table `genres`
--

CREATE TABLE IF NOT EXISTS `genres` (
  `gid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`gid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Table structure for table `movies`
--

CREATE TABLE IF NOT EXISTS `movies` (
  `moid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `runtime` int(11) DEFAULT NULL,
  `launch` datetime DEFAULT NULL,
  `imgurl` varchar(200) DEFAULT NULL,
  `trailer` varchar(200) DEFAULT NULL,
  `pgid` int(11) DEFAULT NULL,
  `description` text NOT NULL,
  PRIMARY KEY (`moid`),
  KEY `pgid` (`pgid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

-- --------------------------------------------------------

--
-- Table structure for table `movie_genre`
--

CREATE TABLE IF NOT EXISTS `movie_genre` (
  `mgid` int(11) NOT NULL AUTO_INCREMENT,
  `moid` int(11) NOT NULL,
  `gid` int(11) NOT NULL,
  PRIMARY KEY (`mgid`),
  UNIQUE KEY `moid_2` (`moid`,`gid`),
  KEY `gid` (`gid`),
  KEY `moid` (`moid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE IF NOT EXISTS `orders` (
  `orid` int(11) NOT NULL AUTO_INCREMENT,
  `shid` int(11) DEFAULT NULL,
  `usid` int(11) DEFAULT NULL,
  `tickets` int(11) DEFAULT NULL,
  `prid` int(11) DEFAULT NULL,
  PRIMARY KEY (`orid`),
  KEY `shid` (`shid`),
  KEY `usid` (`usid`),
  KEY `prid` (`prid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

-- --------------------------------------------------------

--
-- Table structure for table `people`
--

CREATE TABLE IF NOT EXISTS `people` (
  `peid` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(50) DEFAULT NULL,
  `lname` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`peid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Table structure for table `people_movies`
--

CREATE TABLE IF NOT EXISTS `people_movies` (
  `pmid` int(11) NOT NULL AUTO_INCREMENT,
  `peid` int(11) NOT NULL,
  `moid` int(11) NOT NULL,
  `rid` int(11) NOT NULL,
  PRIMARY KEY (`pmid`),
  KEY `moid` (`moid`),
  KEY `rid` (`rid`),
  KEY `peid_2` (`peid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

-- --------------------------------------------------------

--
-- Table structure for table `pgs`
--

CREATE TABLE IF NOT EXISTS `pgs` (
  `pgid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `description` text,
  `pgsid` int(11) DEFAULT NULL,
  `age_from` int(11) DEFAULT NULL,
  `age_to` int(11) DEFAULT NULL,
  PRIMARY KEY (`pgid`),
  KEY `pgsid` (`pgsid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Table structure for table `pg_systems`
--

CREATE TABLE IF NOT EXISTS `pg_systems` (
  `pgsid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`pgsid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Table structure for table `prices`
--

CREATE TABLE IF NOT EXISTS `prices` (
  `prid` int(11) NOT NULL AUTO_INCREMENT,
  `price` float DEFAULT NULL,
  `title` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`prid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE IF NOT EXISTS `role` (
  `rid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  PRIMARY KEY (`rid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Table structure for table `shows`
--

CREATE TABLE IF NOT EXISTS `shows` (
  `shid` int(11) NOT NULL AUTO_INCREMENT,
  `show_start` datetime DEFAULT NULL,
  `show_end` datetime DEFAULT NULL,
  `moid` int(11) DEFAULT NULL,
  `tid` int(11) DEFAULT NULL,
  `prid` int(11) DEFAULT NULL,
  PRIMARY KEY (`shid`),
  KEY `moid` (`moid`),
  KEY `tid` (`tid`),
  KEY `prid` (`prid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Table structure for table `theaters`
--

CREATE TABLE IF NOT EXISTS `theaters` (
  `tid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `seats` int(11) DEFAULT NULL,
  PRIMARY KEY (`tid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `usid` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(50) DEFAULT NULL,
  `lname` varchar(50) DEFAULT NULL,
  `passwd` varchar(20) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `discount` float DEFAULT NULL,
  PRIMARY KEY (`usid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `movies`
--
ALTER TABLE `movies`
  ADD CONSTRAINT `movies_ibfk_1` FOREIGN KEY (`pgid`) REFERENCES `pgs` (`pgid`) ON DELETE SET NULL;

--
-- Constraints for table `movie_genre`
--
ALTER TABLE `movie_genre`
  ADD CONSTRAINT `movie_genre_ibfk_1` FOREIGN KEY (`moid`) REFERENCES `movies` (`moid`) ON DELETE CASCADE,
  ADD CONSTRAINT `movie_genre_ibfk_2` FOREIGN KEY (`gid`) REFERENCES `genres` (`gid`) ON DELETE CASCADE;

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_3` FOREIGN KEY (`prid`) REFERENCES `prices` (`prid`),
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`shid`) REFERENCES `shows` (`shid`),
  ADD CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`usid`) REFERENCES `users` (`usid`);

--
-- Constraints for table `people_movies`
--
ALTER TABLE `people_movies`
  ADD CONSTRAINT `people_movies_ibfk_1` FOREIGN KEY (`peid`) REFERENCES `people` (`peid`) ON DELETE CASCADE,
  ADD CONSTRAINT `people_movies_ibfk_2` FOREIGN KEY (`moid`) REFERENCES `movies` (`moid`) ON DELETE CASCADE,
  ADD CONSTRAINT `people_movies_ibfk_3` FOREIGN KEY (`rid`) REFERENCES `role` (`rid`) ON DELETE CASCADE;

--
-- Constraints for table `pgs`
--
ALTER TABLE `pgs`
  ADD CONSTRAINT `pgs_ibfk_1` FOREIGN KEY (`pgsid`) REFERENCES `pg_systems` (`pgsid`) ON DELETE CASCADE;

--
-- Constraints for table `shows`
--
ALTER TABLE `shows`
  ADD CONSTRAINT `shows_ibfk_1` FOREIGN KEY (`moid`) REFERENCES `movies` (`moid`) ON DELETE CASCADE,
  ADD CONSTRAINT `shows_ibfk_2` FOREIGN KEY (`tid`) REFERENCES `theaters` (`tid`) ON DELETE CASCADE,
  ADD CONSTRAINT `shows_ibfk_3` FOREIGN KEY (`prid`) REFERENCES `prices` (`prid`) ON DELETE SET NULL;
