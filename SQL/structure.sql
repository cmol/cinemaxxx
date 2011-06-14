-- MySQL dump 10.13  Distrib 5.1.54, for debian-linux-gnu (i686)
--
-- Host: 172.16.0.1    Database: cinemaxxx
-- ------------------------------------------------------
-- Server version	5.1.41-3ubuntu12.10

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `genres`
--

DROP TABLE IF EXISTS `genres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `genres` (
  `gid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`gid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genres`
--

LOCK TABLES `genres` WRITE;
/*!40000 ALTER TABLE `genres` DISABLE KEYS */;
INSERT INTO `genres` VALUES (1,'A Genre');
/*!40000 ALTER TABLE `genres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movie_genre`
--

DROP TABLE IF EXISTS `movie_genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movie_genre` (
  `mgid` int(11) NOT NULL AUTO_INCREMENT,
  `moid` int(11) NOT NULL,
  `gid` int(11) NOT NULL,
  PRIMARY KEY (`mgid`),
  UNIQUE KEY `moid_2` (`moid`,`gid`),
  KEY `gid` (`gid`),
  KEY `moid` (`moid`),
  CONSTRAINT `movie_genre_ibfk_1` FOREIGN KEY (`moid`) REFERENCES `movies` (`moid`) ON DELETE CASCADE,
  CONSTRAINT `movie_genre_ibfk_2` FOREIGN KEY (`gid`) REFERENCES `genres` (`gid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movie_genre`
--

LOCK TABLES `movie_genre` WRITE;
/*!40000 ALTER TABLE `movie_genre` DISABLE KEYS */;
INSERT INTO `movie_genre` VALUES (2,10,2);
/*!40000 ALTER TABLE `movie_genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movies`
--

DROP TABLE IF EXISTS `movies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `movies` (
  `moid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `runtime` int(11) DEFAULT NULL,
  `launch` datetime DEFAULT NULL,
  `imgurl` varchar(200) DEFAULT NULL,
  `trailer` varchar(200) DEFAULT NULL,
  `pgid` int(11) DEFAULT NULL,
  `description` text NOT NULL,
  PRIMARY KEY (`moid`),
  KEY `pgid` (`pgid`),
  CONSTRAINT `movies_ibfk_1` FOREIGN KEY (`pgid`) REFERENCES `pgs` (`pgid`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movies`
--

LOCK TABLES `movies` WRITE;
/*!40000 ALTER TABLE `movies` DISABLE KEYS */;
INSERT INTO `movies` VALUES (1,'They Live',100,'0001-01-01 00:00:00','http://cf1.imgobject.com/posters/21f/4bc9211e017a3c57fe00d21f/they-live-cover.jpg','trailer.com',2,'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque accumsan rhoncus dolor, vel facilisis massa imperdiet at. Sed tempor, nulla id fringilla elementum, justo nisl ultricies ipsum, ac congue nisl lacus lacinia nisl. Donec tincidunt, mi ut rhoncus fringilla, sem mauris consequat erat, et accumsan lorem lorem vel neque. Suspendisse felis mauris, cursus et mattis nec, ultricies sit amet erat. Ut quis augue ut magna varius hendrerit. Duis auctor gravida mi, nec laoreet sem tristique aliquet. Quisque a elit diam. Curabitur quis lorem vitae odio pretium gravida. Maecenas molestie, tellus at consectetur facilisis, nisl est tincidunt leo, at luctus nisl quam vel dui. Praesent sed tincidunt dolor. Maecenas mauris neque, volutpat sit amet lobortis in, aliquet non leo. Suspendisse quis lectus eu massa luctus pellentesque sed a leo. Aliquam eros orci, lacinia ac cursus at, molestie eu tortor. Fusce id nunc odio. Praesent tempor luctus dolor, vitae luctus nunc vestibulum in. Proin enim elit, ultricies in posuere non, scelerisque ac magna. Suspendisse velit tortor, laoreet sed ultrices ac, accumsan ut libero. Duis vel ornare tellus. Phasellus sollicitudin faucibus mi, porttitor placerat neque suscipit nec.\r\n\r\nInteger adipiscing leo ligula. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nam sit amet felis sed ligula blandit tincidunt at ut libero. Nullam viverra iaculis ipsum vitae egestas. Sed quis justo libero, a hendrerit lorem. Sed mollis, ipsum id luctus pellentesque, dui quam gravida nibh, non accumsan nibh eros quis tortor. Morbi at ullamcorper elit. Curabitur fermentum feugiat dictum. Nullam eu nisi dui, et sollicitudin tortor. Nullam ac porta justo. Phasellus rutrum aliquam varius. Duis ornare accumsan risus, id vehicula diam accumsan ac. Suspendisse potenti. Ut feugiat sodales suscipit. Nullam dignissim, metus ac sagittis luctus, velit purus rhoncus mi, a bibendum justo elit et massa. Donec pellentesque congue sem, dignissim commodo ante commodo in. Duis et elit purus. Proin molestie placerat tincidunt. Morbi orci elit, tempus nec ultricies sodales, aliquam sed libero.\r\n\r\nDonec ullamcorper, sapien vitae elementum tristique, nibh odio sollicitudin enim, nec ultrices dui neque eget risus. Vivamus sem est, lobortis elementum ultrices in, egestas a tortor. Etiam congue nunc a eros vehicula eget elementum velit lacinia. Aliquam sem dui, eleifend non hendrerit nec, eleifend vel mi. Aenean malesuada faucibus neque. In sapien augue, pellentesque id auctor vitae, vestibulum vel dui. Donec faucibus justo sed nisi rutrum ullamcorper adipiscing ipsum semper. Proin at felis arcu. Aenean in velit vel tellus volutpat semper non vel ipsum. Duis et facilisis arcu.\r\n\r\nEtiam mattis tristique neque, sit amet hendrerit leo adipiscing in. Aliquam aliquet, velit in faucibus blandit, felis leo laoreet leo, quis tempor elit libero sit amet lectus. Sed ante libero, viverra vitae sodales in, fermentum et nibh. Integer lobortis aliquet pellentesque. Integer ullamcorper, mi feugiat pretium mattis, metus velit accumsan orci, quis rutrum velit lorem egestas nunc. Proin placerat, lectus sit amet condimentum euismod, tellus turpis bibendum libero, ac condimentum arcu tortor vitae nisi. Morbi pharetra varius volutpat. Vivamus dictum, enim ac vehicula congue, mauris ante luctus libero, tempor gravida arcu enim at leo. Vestibulum orci ipsum, ultrices id varius vel, porttitor sit amet tellus. In tellus nunc, posuere vitae laoreet a, facilisis a tellus. Nunc molestie ipsum eu tellus dictum non tempus diam rhoncus. Donec pretium vestibulum bibendum. Phasellus nunc turpis, laoreet et consequat ac, auctor ac lorem. Donec feugiat laoreet ipsum, in molestie enim sagittis lacinia. Curabitur venenatis viverra magna at pretium. Suspendisse in enim neque. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Morbi eget sem ut leo pharetra tempor nec eget lacus.\r\n\r\nDuis non dui nec felis tristique convallis non id justo. Ut id felis nec lectus viverra tempus. Etiam vel orci lorem. Donec congue placerat nibh, nec sollicitudin lacus tempus ut. Vestibulum cursus metus aliquet lorem vehicula eu sodales odio iaculis. Sed aliquam aliquet vulputate. Donec malesuada mattis metus, euismod imperdiet quam porta non. Morbi dolor tortor, tincidunt sed suscipit ac, vestibulum eu tortor. Phasellus sagittis nulla eu ante congue faucibus. Mauris sollicitudin luctus mattis. Suspendisse velit arcu, lacinia id ornare vel, sollicitudin in arcu. ');
/*!40000 ALTER TABLE `movies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `orid` int(11) NOT NULL AUTO_INCREMENT,
  `shid` int(11) DEFAULT NULL,
  `usid` int(11) DEFAULT NULL,
  `tickets` int(11) DEFAULT NULL,
  `prid` int(11) DEFAULT NULL,
  `price` float NOT NULL,
  PRIMARY KEY (`orid`),
  KEY `shid` (`shid`),
  KEY `usid` (`usid`),
  KEY `prid` (`prid`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`shid`) REFERENCES `shows` (`shid`),
  CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`usid`) REFERENCES `users` (`usid`),
  CONSTRAINT `orders_ibfk_3` FOREIGN KEY (`prid`) REFERENCES `prices` (`prid`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (4,1,1,3,1,0);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `people`
--

DROP TABLE IF EXISTS `people`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `people` (
  `peid` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(50) DEFAULT NULL,
  `lname` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`peid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `people`
--

LOCK TABLES `people` WRITE;
/*!40000 ALTER TABLE `people` DISABLE KEYS */;
INSERT INTO `people` VALUES (2,'Lars','Larsen');
/*!40000 ALTER TABLE `people` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `people_movies`
--

DROP TABLE IF EXISTS `people_movies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `people_movies` (
  `pmid` int(11) NOT NULL AUTO_INCREMENT,
  `peid` int(11) NOT NULL,
  `moid` int(11) NOT NULL,
  `rid` int(11) NOT NULL,
  PRIMARY KEY (`pmid`),
  KEY `moid` (`moid`),
  KEY `rid` (`rid`),
  KEY `peid_2` (`peid`),
  CONSTRAINT `people_movies_ibfk_1` FOREIGN KEY (`peid`) REFERENCES `people` (`peid`) ON DELETE CASCADE,
  CONSTRAINT `people_movies_ibfk_2` FOREIGN KEY (`moid`) REFERENCES `movies` (`moid`) ON DELETE CASCADE,
  CONSTRAINT `people_movies_ibfk_3` FOREIGN KEY (`rid`) REFERENCES `role` (`rid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `people_movies`
--

LOCK TABLES `people_movies` WRITE;
/*!40000 ALTER TABLE `people_movies` DISABLE KEYS */;
INSERT INTO `people_movies` VALUES (6,2,2,1);
/*!40000 ALTER TABLE `people_movies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pg_systems`
--

DROP TABLE IF EXISTS `pg_systems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pg_systems` (
  `pgsid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`pgsid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pg_systems`
--

LOCK TABLES `pg_systems` WRITE;
/*!40000 ALTER TABLE `pg_systems` DISABLE KEYS */;
INSERT INTO `pg_systems` VALUES (1,'MCfCaY');
/*!40000 ALTER TABLE `pg_systems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pgs`
--

DROP TABLE IF EXISTS `pgs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pgs` (
  `pgid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `description` text,
  `pgsid` int(11) DEFAULT NULL,
  `age_from` int(11) DEFAULT NULL,
  `age_to` int(11) DEFAULT NULL,
  PRIMARY KEY (`pgid`),
  KEY `pgsid` (`pgsid`),
  CONSTRAINT `pgs_ibfk_1` FOREIGN KEY (`pgsid`) REFERENCES `pg_systems` (`pgsid`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pgs`
--

LOCK TABLES `pgs` WRITE;
/*!40000 ALTER TABLE `pgs` DISABLE KEYS */;
INSERT INTO `pgs` VALUES (1,'A','Forbudt for alle',1,1,6);
/*!40000 ALTER TABLE `pgs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prices`
--

DROP TABLE IF EXISTS `prices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `prices` (
  `prid` int(11) NOT NULL AUTO_INCREMENT,
  `price` float DEFAULT NULL,
  `title` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`prid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prices`
--

LOCK TABLES `prices` WRITE;
/*!40000 ALTER TABLE `prices` DISABLE KEYS */;
INSERT INTO `prices` VALUES (1,50,'Meget dyr');
/*!40000 ALTER TABLE `prices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `rid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  PRIMARY KEY (`rid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Instruktør');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shows`
--

DROP TABLE IF EXISTS `shows`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `shows` (
  `shid` int(11) NOT NULL AUTO_INCREMENT,
  `show_start` datetime DEFAULT NULL,
  `show_end` datetime DEFAULT NULL,
  `moid` int(11) DEFAULT NULL,
  `tid` int(11) DEFAULT NULL,
  `prid` int(11) DEFAULT NULL,
  PRIMARY KEY (`shid`),
  KEY `moid` (`moid`),
  KEY `tid` (`tid`),
  KEY `prid` (`prid`),
  CONSTRAINT `shows_ibfk_1` FOREIGN KEY (`moid`) REFERENCES `movies` (`moid`) ON DELETE CASCADE,
  CONSTRAINT `shows_ibfk_2` FOREIGN KEY (`tid`) REFERENCES `theaters` (`tid`) ON DELETE CASCADE,
  CONSTRAINT `shows_ibfk_3` FOREIGN KEY (`prid`) REFERENCES `prices` (`prid`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shows`
--

LOCK TABLES `shows` WRITE;
/*!40000 ALTER TABLE `shows` DISABLE KEYS */;
INSERT INTO `shows` VALUES (1,'0001-01-01 00:00:00','0001-01-01 00:00:00',1,1,1);
/*!40000 ALTER TABLE `shows` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `theaters`
--

DROP TABLE IF EXISTS `theaters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `theaters` (
  `tid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) DEFAULT NULL,
  `seats` int(11) DEFAULT NULL,
  PRIMARY KEY (`tid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theaters`
--

LOCK TABLES `theaters` WRITE;
/*!40000 ALTER TABLE `theaters` DISABLE KEYS */;
INSERT INTO `theaters` VALUES (1,'BIO 1',50);
/*!40000 ALTER TABLE `theaters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `usid` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(50) DEFAULT NULL,
  `lname` varchar(50) DEFAULT NULL,
  `passwd` varchar(20) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `discount` float DEFAULT NULL,
  PRIMARY KEY (`usid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Root','User','1234','12345678','admin@here.com',1);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2011-06-14 10:31:08
