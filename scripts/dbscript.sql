CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Order" (
    "Id" uuid NOT NULL,
    "ProductDescription" character varying(200) NOT NULL,
    "ProductValue" numeric(8,2) NOT NULL,
    "ProductQuantity" integer NOT NULL,
    "UserEmail" character varying(100) NOT NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "LastUpdate" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Order" PRIMARY KEY ("Id")
);

CREATE TABLE "Payment" (
    "Id" uuid NOT NULL,
    "OrderId" uuid NOT NULL,
    "Status" integer NOT NULL,
    "CreatedOn" timestamp with time zone NOT NULL,
    "LastUpdate" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Payment" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Payment_Order_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Order" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CreditCard" (
    "Id" uuid NOT NULL,
    "Number" character varying(20) NOT NULL,
    "CVV" character varying(3) NOT NULL,
    "NumberOfInstallment" integer NOT NULL,
    "ValuePerInstallment" numeric(8,2) NOT NULL,
    CONSTRAINT "PK_CreditCard" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CreditCard_Payment_Id" FOREIGN KEY ("Id") REFERENCES "Payment" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Payment_OrderId" ON "Payment" ("OrderId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230913235819_Initial', '6.0.5');

COMMIT;

