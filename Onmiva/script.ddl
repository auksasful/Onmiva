#@(#) script.ddl

DROP TABLE IF EXISTS Apmokejimai;
DROP TABLE IF EXISTS Uzsakymo_eilutes;
DROP TABLE IF EXISTS Team_building_pasiulymai_darbuotojui;
DROP TABLE IF EXISTS Saskaitos;
DROP TABLE IF EXISTS Pasiulymai_brigadininkui;
DROP TABLE IF EXISTS Brigadininku_atostogu_laikai;
DROP TABLE IF EXISTS Darbuotoju_atostogu_laikai;
DROP TABLE IF EXISTS Uzsakymai;
DROP TABLE IF EXISTS Team_Building_pasiulymas;
DROP TABLE IF EXISTS Siuntu_imones_darbuotojai;
DROP TABLE IF EXISTS Siuntu_imones_brigadininkai;
DROP TABLE IF EXISTS Siuntejo_ivertinimai;
DROP TABLE IF EXISTS Siuntejo_atsiliepimai;
DROP TABLE IF EXISTS Rezervacija_ivairiais_klausimais;
DROP TABLE IF EXISTS Kontraktai;
DROP TABLE IF EXISTS Darbai;
DROP TABLE IF EXISTS Atostogu_laikas;
DROP TABLE IF EXISTS Brigadininku_darbo_grafikai;
DROP TABLE IF EXISTS Darbuotojo_darbo_grafikai;
DROP TABLE IF EXISTS Brigadininko_darbo_grafikai;
DROP TABLE IF EXISTS Uzsakymu_busenos;
DROP TABLE IF EXISTS Saskaitos_busenos;
DROP TABLE IF EXISTS Apmokejimo_metodai;
DROP TABLE IF EXISTS Uzsakymo_busenos;
DROP TABLE IF EXISTS Siuntu_imones;
DROP TABLE IF EXISTS Siuntejai;
DROP TABLE IF EXISTS Marsrutai;
DROP TABLE IF EXISTS Klientai;
DROP TABLE IF EXISTS Brigadininkai;
DROP TABLE IF EXISTS Apmokejimo_metodai;
DROP TABLE IF EXISTS Darbuotojai;
DROP TABLE IF EXISTS Darbo_grafikai;
DROP TABLE IF EXISTS Administracija;
CREATE TABLE Administracija
(
	asmens_kodas integer NOT NULL AUTO_INCREMENT,
	vardas varchar(255) NOT NULL,
	pavarde varchar(255) NOT NULL,
	uzimamos_pareigos varchar(255) NOT NULL,
	darbo_stazas integer NOT NULL,
	PRIMARY KEY(asmens_kodas)
);

CREATE TABLE Darbo_grafikai
(
	pamainu_laikai date NOT NULL,
	id_Darbo_grafikas integer NOT NULL AUTO_INCREMENT,
	PRIMARY KEY(id_Darbo_grafikas)
);

CREATE TABLE Darbuotojai
(
	tebelio_nr varchar(255) NOT NULL,
	vardas varchar(255) NOT NULL,
	telefonas varchar(255) NOT NULL,
	PRIMARY KEY(tebelio_nr)
);


CREATE TABLE Brigadininkai
(
	tebelio_nr varchar(255) NOT NULL,
	vardas varchar(255) NOT NULL,
	telefonas varchar(255) NOT NULL,
	PRIMARY KEY(tebelio_nr)
);

CREATE TABLE Klientai
(
	vardas varchar(255) NOT NULL,
	pavarde varchar(255) NOT NULL,
	el_pastas varchar(255) NOT NULL,
	slaptazodis varchar(255) NOT NULL,
	role varchar(255) NOT NULL,
	patvirtintas int NOT NULL,
	patvirtinimo_kodas varchar(255) NOT NULL,
	id_Klientas integer NOT NULL AUTO_INCREMENT,
	PRIMARY KEY(id_Klientas)
);

CREATE TABLE Marsrutai
(
	id_Marsrutas integer NOT NULL AUTO_INCREMENT,
	pavadinimas varchar(255),
	PRIMARY KEY(id_Marsrutas)
);

CREATE TABLE Siuntejai
(
	id integer NOT NULL AUTO_INCREMENT,
	pavadinimas varchar (255) NOT NULL,
	tel_nr varchar (255) NOT NULL,
	miestas varchar (255) NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE Siuntu_imones
(
	id integer NOT NULL AUTO_INCREMENT,
	pavadinimas varchar(255) NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE Apmokejimo_metodai
(
	id_Apmokejimo_metodai integer NOT NULL AUTO_INCREMENT,
	name char (20) NOT NULL,
	PRIMARY KEY(id_Apmokejimo_metodai)
);
INSERT INTO Apmokejimo_metodai(id_Apmokejimo_metodai, name) VALUES(1, 'banko pavedimu');
INSERT INTO Apmokejimo_metodai(id_Apmokejimo_metodai, name) VALUES(2, 'tiesiogiai kurjeriui');

CREATE TABLE Saskaitos_busenos
(
	id_Saskaitos_busenos integer NOT NULL,
	name char (32) NOT NULL,
	PRIMARY KEY(id_Saskaitos_busenos)
);
INSERT INTO Saskaitos_busenos(id_Saskaitos_busenos, name) VALUES(1, 'apmoketa');
INSERT INTO Saskaitos_busenos(id_Saskaitos_busenos, name) VALUES(2, 'neapmoketa');
INSERT INTO Saskaitos_busenos(id_Saskaitos_busenos, name) VALUES(3, 'laukiama apmokejimo patvirtinimo');

CREATE TABLE Uzsakymu_busenos
(
	id_Užsakymo_busenos integer NOT NULL,
	name char (11) NOT NULL,
	PRIMARY KEY(id_Užsakymo_busenos)
);
INSERT INTO Uzsakymu_busenos(id_Užsakymo_busenos, name) VALUES(1, 'atidaryas');
INSERT INTO Uzsakymu_busenos(id_Užsakymo_busenos, name) VALUES(2, 'pateiktas');
INSERT INTO Uzsakymu_busenos(id_Užsakymo_busenos, name) VALUES(3, 'archyvuotas');

CREATE TABLE Brigadininko_darbo_grafikai
(
	fk_Darbo_grafikasid_Darbo_grafikas integer NOT NULL,
	fk_Brigadininkastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Darbo_grafikasid_Darbo_grafikas, fk_Brigadininkastebelio_nr),
	UNIQUE(fk_Brigadininkastebelio_nr),
	CONSTRAINT Turi1 FOREIGN KEY(fk_Darbo_grafikasid_Darbo_grafikas) REFERENCES Darbo_grafikai (id_Darbo_grafikas)
);

CREATE TABLE Darbuotojo_darbo_grafikai
(
	fk_Darbo_grafikasid_Darbo_grafikas integer NOT NULL,
	fk_Darbuotojastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Darbo_grafikasid_Darbo_grafikas, fk_Darbuotojastebelio_nr),
	UNIQUE(fk_Darbuotojastebelio_nr),
	CONSTRAINT Turi2 FOREIGN KEY(fk_Darbo_grafikasid_Darbo_grafikas) REFERENCES Darbo_grafikai (id_Darbo_grafikas)
);

CREATE TABLE Brigadininku_darbo_grafikai
(
	fk_Darbo_grafikasid_Darbo_grafikas integer NOT NULL,
	fk_Brigadininkastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Darbo_grafikasid_Darbo_grafikas, fk_Brigadininkastebelio_nr),
	CONSTRAINT Kuria FOREIGN KEY(fk_Darbo_grafikasid_Darbo_grafikas) REFERENCES Darbo_grafikai (id_Darbo_grafikas)
);

CREATE TABLE Atostogu_laikas
(
	Pradzios_data date NOT NULL,
	Pabaigos_data date NOT NULL,
	Tipas varchar(255) NOT NULL,
	Apmokejimo_procentas integer NOT NULL,
	id_Atostogu_laikas integer NOT NULL AUTO_INCREMENT,
	fk_Administracijaasmens_kodas integer NOT NULL,
	PRIMARY KEY(id_Atostogu_laikas),
	CONSTRAINT administruoja FOREIGN KEY(fk_Administracijaasmens_kodas) REFERENCES Administracija (asmens_kodas)
);

CREATE TABLE Darbai
(
	Atlikimo_data date NOT NULL,
	Svoris decimal NOT NULL,
	id_Darbas integer NOT NULL AUTO_INCREMENT,
	fk_Klientasid_Klientas integer NOT NULL,
	fk_Marsrutasid_Marsrutas integer NOT NULL,
	fk_Brigadininkastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(id_Darbas),
	CONSTRAINT Užskao FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas),
	CONSTRAINT priklauso FOREIGN KEY(fk_Marsrutasid_Marsrutas) REFERENCES Marsrutai (id_Marsrutas),
	CONSTRAINT Kuria_priskiria_naikina FOREIGN KEY(fk_Brigadininkastebelio_nr) REFERENCES Brigadininkai (tebelio_nr)
);

CREATE TABLE Kontraktai
(
	id integer NOT NULL AUTO_INCREMENT,
	pasirasymo_data date NOT NULL,
	galioja_iki date NOT NULL,
	fk_Siuntu_imoneid integer NOT NULL,
	fk_Siuntejasid integer NOT NULL,
	PRIMARY KEY(id),
	UNIQUE(fk_Siuntejasid),
	CONSTRAINT pasiraso FOREIGN KEY(fk_Siuntu_imoneid) REFERENCES Siuntu_imones (id),
	CONSTRAINT pasirašo FOREIGN KEY(fk_Siuntejasid) REFERENCES Siuntejai (id)
);

CREATE TABLE Rezervacija_ivairiais_klausimais
(
	pavadinimas varchar(255) NOT NULL,
	aprasas varchar(255) NOT NULL,
	data date NOT NULL,
	id_Rezervacija_ivairiais_klausimais integer NOT NULL AUTO_INCREMENT,
	fk_Klientasid_Klientas integer NOT NULL,
	fk_Administracijaasmens_kodas integer NOT NULL,
	PRIMARY KEY(id_Rezervacija_ivairiais_klausimais),
	CONSTRAINT sukuria FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas),
	CONSTRAINT priima FOREIGN KEY(fk_Administracijaasmens_kodas) REFERENCES Administracija (asmens_kodas)
);

CREATE TABLE Siuntejo_atsiliepimai
(
	id integer NOT NULL AUTO_INCREMENT,
	atsiliepimas varchar(255) NOT NULL,
	fk_Klientasid_Klientas integer NOT NULL,
	fk_Siuntejasid integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT palieka FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas),
	CONSTRAINT gauna1 FOREIGN KEY(fk_Siuntejasid) REFERENCES Siuntejai (id)
);

CREATE TABLE Siuntejo_ivertinimai
(
	id integer AUTO_INCREMENT,
	ivertinimas integer,
	fk_Siuntejasid integer NOT NULL,
	fk_Klientasid_Klientas integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT gauna2 FOREIGN KEY(fk_Siuntejasid) REFERENCES Siuntejai (id),
	CONSTRAINT parašo FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas)
);

CREATE TABLE Siuntu_imones_brigadininkai
(
	fk_Brigadininkastebelio_nr varchar(255) NOT NULL,
	fk_Siuntu_imoneid integer NOT NULL,
	PRIMARY KEY(fk_Brigadininkastebelio_nr, fk_Siuntu_imoneid),
	UNIQUE(fk_Brigadininkastebelio_nr),
	CONSTRAINT samdo FOREIGN KEY(fk_Brigadininkastebelio_nr) REFERENCES Brigadininkai (tebelio_nr)
);

CREATE TABLE Siuntu_imones_darbuotojai
(
	fk_Darbuotojastebelio_nr varchar(255) NOT NULL,
	fk_Siuntu_imoneid integer NOT NULL,
	PRIMARY KEY(fk_Darbuotojastebelio_nr, fk_Siuntu_imoneid),
	UNIQUE(fk_Darbuotojastebelio_nr),
	CONSTRAINT samdo1 FOREIGN KEY(fk_Darbuotojastebelio_nr) REFERENCES Darbuotojai (tebelio_nr)
);

CREATE TABLE Team_Building_pasiulymas
(
	id integer NOT NULL AUTO_INCREMENT,
	data date NOT NULL,
	skyrius varchar(255) NOT NULL,
	preliminarios_islaidos integer NOT NULL,
	fk_Administracijaasmens_kodas integer NOT NULL,
	PRIMARY KEY(id),
	CONSTRAINT peržiuri FOREIGN KEY(fk_Administracijaasmens_kodas) REFERENCES Administracija (asmens_kodas)
);

CREATE TABLE Uzsakymai
(
	id integer NOT NULL AUTO_INCREMENT,
	data date NOT NULL,
	suma decimal NOT NULL,
	svoris decimal NOT NULL,
	busena integer NOT NULL,
	fk_Siuntu_imoneid integer NOT NULL,
	fk_Klientasid_Klientas integer NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(busena) REFERENCES Uzsakymu_busenos (id_Užsakymo_busenos),
	CONSTRAINT perduodamas FOREIGN KEY(fk_Siuntu_imoneid) REFERENCES Siuntu_imones (id),
	CONSTRAINT pateikia FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas)
);

CREATE TABLE Darbuotoju_atostogu_laikai
(
	fk_Atostogu_laikasid_Atostogu_laikas integer NOT NULL,
	fk_Darbuotojastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Atostogu_laikasid_Atostogu_laikas, fk_Darbuotojastebelio_nr),
	UNIQUE(fk_Darbuotojastebelio_nr),
	CONSTRAINT gauna3 FOREIGN KEY(fk_Atostogu_laikasid_Atostogu_laikas) REFERENCES Atostogu_laikas (id_Atostogu_laikas)
);

CREATE TABLE Brigadininku_atostogu_laikai
(
	fk_Atostogu_laikasid_Atostogu_laikas integer NOT NULL,
	fk_Brigadininkastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Atostogu_laikasid_Atostogu_laikas, fk_Brigadininkastebelio_nr),
	UNIQUE(fk_Brigadininkastebelio_nr),
	CONSTRAINT gauna4 FOREIGN KEY(fk_Atostogu_laikasid_Atostogu_laikas) REFERENCES Atostogu_laikas (id_Atostogu_laikas)
);

CREATE TABLE Pasiulymai_brigadininkui
(
	fk_Team_Building_pasiulymasid integer NOT NULL,
	fk_Brigadininkastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Team_Building_pasiulymasid, fk_Brigadininkastebelio_nr),
	UNIQUE(fk_Team_Building_pasiulymasid),
	CONSTRAINT paraso FOREIGN KEY(fk_Team_Building_pasiulymasid) REFERENCES Team_Building_pasiulymas (id)
);

CREATE TABLE Saskaitos
(
	nr integer NOT NULL,
	suma decimal NOT NULL,
	data date NOT NULL,
	mokejimo_paskirtis varchar(255) NOT NULL,
	apmoketi_iki date NOT NULL,
	busena integer NOT NULL,
	fk_Klientasid_Klientas integer NOT NULL,
	fk_Uzsakymasid integer NOT NULL,
	fk_Siuntu_imoneid integer NOT NULL,
	PRIMARY KEY(nr),
	UNIQUE(fk_Uzsakymasid),
	FOREIGN KEY(busena) REFERENCES Saskaitos_busenos (id_Saskaitos_busenos),
	CONSTRAINT perduodama FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas),
	CONSTRAINT atitinka FOREIGN KEY(fk_Uzsakymasid) REFERENCES Uzsakymai (id),
	CONSTRAINT pateikia1 FOREIGN KEY(fk_Siuntu_imoneid) REFERENCES Siuntu_imones (id)
);

CREATE TABLE Team_building_pasiulymai_darbuotojui
(
	fk_Team_Building_pasiulymasid integer NOT NULL AUTO_INCREMENT,
	fk_Darbuotojastebelio_nr varchar(255) NOT NULL,
	PRIMARY KEY(fk_Team_Building_pasiulymasid, fk_Darbuotojastebelio_nr),
	UNIQUE(fk_Team_Building_pasiulymasid),
	CONSTRAINT paraso1 FOREIGN KEY(fk_Team_Building_pasiulymasid) REFERENCES Team_Building_pasiulymas (id)
);

CREATE TABLE Uzsakymo_eilutes
(
	id integer NOT NULL PRIMARY KEY AUTO_INCREMENT,
	suma decimal NOT NULL,
	svoris decimal NOT NULL,
	fk_Uzsakymasid integer NOT NULL,
	CONSTRAINT priklauso1 FOREIGN KEY(fk_Uzsakymasid) REFERENCES Uzsakymai (id)
);

CREATE TABLE Apmokejimai
(
	data date NOT NULL,
	id integer NOT NULL AUTO_INCREMENT,
	suma decimal NOT NULL,
	apmokejimo_metodas integer NOT NULL,
	fk_Siuntu_imoneid integer NOT NULL,
	fk_Saskaitanr integer NOT NULL,
	fk_Klientasid_Klientas integer NOT NULL,
	PRIMARY KEY(id),
	FOREIGN KEY(apmokejimo_metodas) REFERENCES Apmokejimo_metodai (id_Apmokejimo_metodai),
	CONSTRAINT nukreipiamas FOREIGN KEY(fk_Siuntu_imoneid) REFERENCES Siuntu_imones (id),
	CONSTRAINT paremtas_pagal FOREIGN KEY(fk_Saskaitanr) REFERENCES Saskaitos (nr),
	CONSTRAINT atlieka FOREIGN KEY(fk_Klientasid_Klientas) REFERENCES Klientai (id_Klientas)
);
