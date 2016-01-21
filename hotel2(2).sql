-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Хост: 127.0.0.1
-- Время создания: Янв 22 2016 г., 00:03
-- Версия сервера: 5.5.25
-- Версия PHP: 5.3.13

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `hotel2`
--

-- --------------------------------------------------------

--
-- Структура таблицы `book`
--

CREATE TABLE IF NOT EXISTS `book` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `client_id` int(11) DEFAULT NULL,
  `book_date` datetime DEFAULT NULL,
  `from_day` date DEFAULT NULL,
  `till_day` date DEFAULT NULL,
  `adults` int(11) DEFAULT NULL,
  `childs` int(11) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL,
  `info` text,
  PRIMARY KEY (`id`),
  KEY `client_id` (`client_id`),
  KEY `from_day` (`from_day`),
  KEY `till_day` (`till_day`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Дамп данных таблицы `book`
--

INSERT INTO `book` (`id`, `client_id`, `book_date`, `from_day`, `till_day`, `adults`, `childs`, `status`, `info`) VALUES
(2, 1, '2015-12-08 00:00:00', '2016-01-01', '2016-01-05', 2, 0, NULL, 'Happy New Year!');

-- --------------------------------------------------------

--
-- Структура таблицы `calendar`
--

CREATE TABLE IF NOT EXISTS `calendar` (
  `day` date NOT NULL,
  `wend` tinyint(1) DEFAULT NULL,
  `holiday` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`day`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `calendar`
--

INSERT INTO `calendar` (`day`, `wend`, `holiday`) VALUES
('2016-01-01', 1, 1),
('2016-01-02', 1, 1),
('2016-01-05', 1, 0);

-- --------------------------------------------------------

--
-- Структура таблицы `client`
--

CREATE TABLE IF NOT EXISTS `client` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `client` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phone` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `info` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `client`
--

INSERT INTO `client` (`id`, `client`, `email`, `phone`, `address`, `info`) VALUES
(1, 'Shdanow Valerij Vlad', 'w@b.ru', '223-322', 'St Petersburg, Nevskiy Prospect, 28', 'Hi like St Petersburg and Olga!'),
(2, 'Anna Snatkina', 'anna@gmail.com', '223-322', 'Moscow', 'Luxe'),
(3, 'Tatiana', 'tttt@gmail.com', '223-322', 'Saratov', 'Newa');

-- --------------------------------------------------------

--
-- Структура таблицы `map`
--

CREATE TABLE IF NOT EXISTS `map` (
  `room_id` int(11) NOT NULL DEFAULT '0',
  `book_id` int(11) NOT NULL DEFAULT '0',
  `calendar_day` date NOT NULL DEFAULT '0000-00-00',
  `status` varchar(255) DEFAULT NULL,
  `adults` int(11) DEFAULT NULL,
  `childs` int(11) DEFAULT NULL,
  PRIMARY KEY (`room_id`,`book_id`,`calendar_day`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `map`
--

INSERT INTO `map` (`room_id`, `book_id`, `calendar_day`, `status`, `adults`, `childs`) VALUES
(1, 1, '2016-01-01', 'FREE', 0, 0);

-- --------------------------------------------------------

--
-- Структура таблицы `room`
--

CREATE TABLE IF NOT EXISTS `room` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `room` varchar(255) DEFAULT NULL,
  `beds` int(11) DEFAULT NULL,
  `floor` int(11) DEFAULT NULL,
  `step` int(11) DEFAULT NULL,
  `info` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Дамп данных таблицы `room`
--

INSERT INTO `room` (`id`, `room`, `beds`, `floor`, `step`, `info`) VALUES
(1, 'Room 1 ', 1, 1, 1, 'Luxe'),
(2, 'Room 2', 2, 1, 2, ''),
(3, 'Single 201', 1, 2, 3, 'green room');

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `book`
--
ALTER TABLE `book`
  ADD CONSTRAINT `book_ibfk_1` FOREIGN KEY (`client_id`) REFERENCES `client` (`id`),
  ADD CONSTRAINT `book_ibfk_2` FOREIGN KEY (`from_day`) REFERENCES `calendar` (`day`),
  ADD CONSTRAINT `book_ibfk_3` FOREIGN KEY (`till_day`) REFERENCES `calendar` (`day`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
