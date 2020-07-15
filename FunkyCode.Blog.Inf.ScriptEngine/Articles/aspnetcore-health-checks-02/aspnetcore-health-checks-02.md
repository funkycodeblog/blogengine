Once I took a part in project where backend component was developed along with database structure not exactly 100% in sync. Database model used to be scaffolded via Entity Framework Core Database First functionalities. However database team has habit to change tables during development without informing anybody about changes. It was PoC project, tight budget so nobody was interested in innovations and process refactoring.

Therefore after application deployment on target environment I was wondering if all table models are in sync with database. Typically during test phase I was informed about discrepancies. I create simple solution tailored to this situation to simply test there's 100% compatibility between backend and database.

