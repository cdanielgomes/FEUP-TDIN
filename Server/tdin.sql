CREATE TABLE cats
(
  id              INT unsigned NOT NULL AUTO_INCREMENT,
  name            VARCHAR(max) NOT NULL,               
  username        VARCHAR(MAX) NOT NULL UNIQUE,                                     
  PRIMARY KEY     (id)                                  
);