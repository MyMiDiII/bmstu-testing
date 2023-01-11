COPY public."Platform"("Name", "Popularity", "Cost") 
from '/tmp/data/platform_data.csv' delimiter ';';

COPY public."WebHosting"("Name", "PricePerMonth", "SubMonths") 
from '/tmp/data/webhosting_data.csv' delimiter ';';

COPY public."Country"("Name", "LevelOfInterest", "OverallPlayers") 
from '/tmp/data/country_data.csv' delimiter ';';

COPY public."User"("Login", "Password", "Role")
from '/tmp/data/user_data.csv' delimiter ';';

COPY public."Server"("Name", "Ip", "GameName", "Rating", "Status", "HostingID", "PlatformID", "CountryID", "OwnerID") 
from '/tmp/data/server_data.csv' delimiter ';';

COPY public."Player"("Nickname", "HoursPlayed", "LastPlayed") 
from '/tmp/data/player_data.csv' delimiter ';';

COPY public."ServerPlayer"("ServerID", "PlayerID") 
from '/tmp/data/server_player_data.csv' delimiter ';';

COPY public."FavoriteServer"("ServerID", "UserID") 
from '/tmp/data/fav_servs_data.csv' delimiter ';';
