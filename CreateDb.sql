create database if not exists webAppDatabase;
use webAppDatabase;
create table if not EXISTS users
(
    EmployeeNumber varchar(20) PRIMARY KEY,
    Name        varchar(255) not null,
    Email     varchar(255) UNIQUE not null,
    Password        varchar(255) not null,
    IsAdmin        boolean not null DEFAULT false
);
insert into users(EmployeeNumber, Name, Email, Password,IsAdmin) values ('1234','Hans Gruber', 'hans@gruber.net','HashWorkInProgress',true);
insert into users(EmployeeNumber, Name, Email, Password) values ('1235','John McClane', 'jmcclan@nypd.com','UnderConstruction');
insert into users(EmployeeNumber, Name, Email, Password) values ('1236','Colin Powell', 'cpowell@lapd.com','twinkie');
insert into users(EmployeeNumber, Name, Email, Password) values ('1237','Mr Takagi', 'thebigone@thetower.com','GitGudScrb');
insert into users(EmployeeNumber, Name, Email, Password,IsAdmin) values ('0000','Cookie Monster', 'cookies@jar.nom','Cookies123',true);
insert into users(EmployeeNumber, Name, Email, Password) values ('1239','Bob Lacost', 'bobby@lacost.co','Pizza');
insert into users(EmployeeNumber, Name, Email, Password) values ('1240','Lance Wololo', 'wololo@aoe.net','Relic10');
insert into users(EmployeeNumber, Name, Email, Password) values ('1241','Mr Alucard', 'alucard@walachia.com','notDracula');
insert into users(EmployeeNumber, Name, Email, Password) values ('1242','Karl Johan', 'karl@bernadotte.fr','NotAFrenchGuy');
insert into users(EmployeeNumber, Name, Email, Password) values ('1243','Donald Duck', 'donald@duck.ab','giveMoney');

create table if not EXISTS teams
(
    TeamID int auto_increment NOT NULL,
    TeamName varchar(100) NOT NULL,
    PRIMARY KEY (TeamID)
);

insert into teams(TeamID, TeamName) values ('1','Salg');
insert into teams(TeamID, TeamName) values ('2','Administrasjon');
insert into teams(TeamID, TeamName) values ('3','Produksjon Dør');
insert into teams(TeamID, TeamName) values ('4','Logistikk');
insert into teams(TeamID, TeamName) values ('5','Rengjøring');
insert into teams(TeamID, TeamName) values ('6','IT');
insert into teams(TeamID, TeamName) values ('7','Produksjon Rammer');
insert into teams(TeamID, TeamName) values ('8','Produksjon Skillevegg');
insert into teams(TeamID, TeamName) values ('9','Lager i sverige');
insert into teams(TeamID, TeamName) values ('10','Lager Lyngdal');

create table if not EXISTS category
(
    CategoryID int auto_increment NOT NULL,
    CategoryName varchar(100) NOT NULL,
    PRIMARY KEY (CategoryID)
);

insert into category(CategoryID, CategoryName) values ('1','Small Problem');
insert into category(CategoryID, CategoryName) values ('2','Big Problem');
insert into category(CategoryID, CategoryName) values ('3','Expensive');
insert into category(CategoryID, CategoryName) values ('4','In a Hurry');
insert into category(CategoryID, CategoryName) values ('5','Not that important');
insert into category(CategoryID, CategoryName) values ('6','Something');
insert into category(CategoryID, CategoryName) values ('7','Running out of ideas');
insert into category(CategoryID, CategoryName) values ('8','Please help');
insert into category(CategoryID, CategoryName) values ('9','Gib mony');
insert into category(CategoryID, CategoryName) values ('10','Individual');

create table if not EXISTS suggestions
(
    SuggestionID int auto_increment NOT NULL PRIMARY KEY,
    SuggestionMakerID varchar(20) NOT NULL,
    CategoryID int NOT NULL,
    TeamID int NOT NULL,
    Title varchar(100) NOT NULL,
    Description varchar(255) NOT NULL,
    Phase varchar(20) NOT NULL,
    Status varchar(50) NOT NULL,
    TimeStamp timestamp NOT NULL,
    Deadline varchar(50) NOT NULL,
    FOREIGN KEY (SuggestionMakerID) REFERENCES users(EmployeeNumber),
    FOREIGN KEY (TeamID) REFERENCES teams(TeamID),
    FOREIGN KEY (CategoryID) REFERENCES category(CategoryID)
);

insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('101','0000','Need More Cookies','2','1','The lack of cookies is concerning','Do','In Progress','YESTERDAY');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('102','1241','Moving the wood storage','1','9','Moving the wood storage to aile 4 will reduce the walking time','Study','In Progress','18.11.2022');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('103','1236','Swaping door knob','4','3','Swaping door knob from knob a to b will improve door','Plan','Done','01.01.2023');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('104','1234','Some admin stuff','3','4','More admin stuff here','Study','Completed','11.11.2022');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('105','1234','Improve prod privacy wall','3','7','By doing this thing we can improve the efficency of production ','Plan','In Progress','01.12.2022');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('106','1237','Change Transport company','9','4','Changing from A to B we can save a lot of money','Do','In Progress','21.11.2022');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('107','1243','Opening sales in Andeby','3','1','Opening a office in Andeby to sell doors would be benefical to me','Plan','In Progress','09.02.2023');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('108','1242','Upgrading Hardwere','1','6','Upgrading software will make systems runn faster','Do','Completed','02.11.2022');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('109','1240','Moving production of Door A to Line 3','4','3','Door A is the most popular door so moving it to biggest line would improve production','Plan','In Progress','30.03.2023');
insert into suggestions(SuggestionID, SuggestionMakerID, Title, CategoryID, TeamID, Description, Phase, Status, Deadline) 
    values ('110','1235','Alarm clocks','10','3','Because everyone cant come on time we have decided to give you all alarm clocks','Act','In Progress','18.12.2022');


create table if not EXISTS comments
(
    CommentID int auto_increment not null,
    SuggestionID int not null,
    EmployeeNumber varchar(20) not null,
    CommentText varchar(100) not null,
    PRIMARY KEY (CommentID),
    FOREIGN KEY (SuggestionID) REFERENCES suggestions(SuggestionID),
    FOREIGN KEY (EmployeeNumber) REFERENCES users(EmployeeNumber)
);

insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText)
    values ('1','101','1242','Hvorfor trenger vi flere cookies?');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('2','102','1240','Where do we move the current items in aisle 4?');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('3','102','1239','Maybe in the west corner');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('4','104','1236','Admin is important stuff');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('5','106','1237','A comment');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('6','106','1236','Comment 2');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('7','103','1242','Comment 3');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('8','104','1243','fkwjakogkjkwfkawd');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('9','108','1241','dlwakfawkfawkfoawkfaw');
insert into comments(commentID,SuggestionID,EmployeeNumber,CommentText) 
    values ('10','106','1242','23irfksajw0213124');
