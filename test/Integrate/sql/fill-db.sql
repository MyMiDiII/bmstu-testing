INSERT INTO public."Country" ("Name", "LevelOfInterest", "OverallPlayers")
VALUES ('Country1', '1', '1'),
       ('Country2', '2', '2'),
       ('Country3', '3', '3'),
       ('Country4', '4', '4'),
       ('Country5', '5', '5');


INSERT INTO public."Platform" ("Name", "Popularity", "Cost")
VALUES ('Platform1', '1', '1'),
       ('Platform2', '2', '2'),
       ('Platform3', '3', '3'),
       ('Platform4', '4', '4'),
       ('Platform5', '5', '5');


INSERT INTO public."Player" ("Nickname", "HoursPlayed", "LastPlayed")
VALUES ('Player1', '1', '2023-01-01 00:00:00 +00:00'),
       ('Player2', '2', '2023-01-02 00:00:00 +00:00'),
       ('Player3', '3', '2023-01-03 00:00:00 +00:00'),
       ('Player4', '4', '2023-01-04 00:00:00 +00:00'),
       ('Player5', '5', '2023-01-05 00:00:00 +00:00');


INSERT INTO public."User" ("Login", "Password", "Role")
VALUES ('User1', 'Password1', 'Role1'),
       ('User2', 'Password2', 'Role2'),
       ('User3', 'Password3', 'Role3'),
       ('User4', 'Password4', 'Role4'),
       ('User5', 'Password5', 'Role5');


INSERT INTO public."WebHosting" ("Name", "PricePerMonth", "SubMonths")
VALUES ('Hosting1', '1', '1'),
       ('Hosting2', '2', '2'),
       ('Hosting3', '3', '3'),
       ('Hosting4', '4', '4'),
       ('Hosting5', '5', '5');


insert into public."Server" ("Name", "Ip", "GameName", "Rating", "Status", "HostingID", "PlatformID", "CountryID", "OwnerID")
values ('Server1', '1.1.1.1', 'Game1', '1', '0', '1', '1', '1', '1'),
       ('Server2', '1.1.1.2', 'Game2', '2', '0', '2', '2', '2', '2'),
       ('Server3', '1.1.1.3', 'Game3', '3', '1', '3', '3', '3', '3'),
       ('Server4', '1.1.1.4', 'Game4', '4', '1', '4', '4', '4', '4'),
       ('Server5', '1.1.1.5', 'Game5', '5', '2', '5', '5', '5', '5');


INSERT INTO public."FavoriteServer" ("ServerID", "UserID")
values ('1', '1'),
       ('2', '2'),
       ('3', '3'),
       ('4', '4'),
       ('5', '5');


INSERT INTO public."ServerPlayer" ("ServerID", "PlayerID")
VALUES ('1', '1'),
       ('2', '2'),
       ('3', '3'),
       ('4', '4'),
       ('5', '5');
