create table Addresses
(
    Id               int identity
        constraint PK_Addresses
            primary key,
    FirstName        varchar(255) not null,
    LastName         varchar(255) not null,
    Street1          varchar(255) not null,
    Street2          varchar(255),
    City             varchar(255) not null,
    State            varchar(255) not null,
    Country          varchar(255) not null,
    ZipCode          varchar(255) not null,
    IsEnabled        bit          not null,
    LastModifiedDate datetime     not null,
    UserId           int          not null
)
go

create table BlockedUser
(
    Id               int          not null
        primary key,
    Email            varchar(255) not null,
    Shop             varchar(255) not null,
    IsEnabled        bit          not null,
    LastModifiedDate datetime     not null,
    CreatedDate      datetime     not null
)
go

create table Configuration
(
    ConfigurationId int          not null
        primary key,
    Name            varchar(255) not null,
    Type            int          not null,
    Configuration1  text         not null
)
go

create table FilamentBrands
(
    Id          int identity
        constraint PK_FilamentBrands
            primary key,
    Name        nvarchar(100),
    Description nvarchar(max),
    Origin      nvarchar(100),
    IsActive    bit default 1
)
go

create table FilamentGeneralMaterials
(
    Id                int identity
        constraint PK_FilamentGeneralMaterials
            primary key,
    Name              nvarchar(100),
    NozzleTemperature nvarchar(100),
    BedTemperature    nvarchar(100),
    HeatBed           nvarchar(100),
    Description       nvarchar(max),
    IsActive          bit default 1
)
go

create table GeneralColors
(
    Id       int identity
        constraint PK_GeneralColors
            primary key,
    Name     nvarchar(100),
    Hex      nvarchar(10),
    IsActive bit default 1
)
go

create table ImageTypes
(
    Id        int identity
        constraint PK_ImageTypes
            primary key,
    Type      nvarchar(255)                 not null,
    IsEnabled bit      default 1,
    CreatedAt datetime default getutcdate() not null,
    UpdatedAt datetime default getutcdate() not null,
    Width     int                           not null,
    Height    int                           not null
)
go

create table MarketPlaces
(
    Id        int identity
        constraint PK_MarketPlaces
            primary key,
    Name      nvarchar(255),
    CreatedAt datetime default getutcdate(),
    UpdatedAt datetime default getutcdate()
)
go

create table ModelCategories
(
    Id        int identity
        constraint PK_ThreeDModelCategories
            primary key,
    Name      nvarchar(100),
    Parent    int
        constraint FK_ThreeDModelCategories_Parent
            references ModelCategories,
    IsEnabled bit default 1 not null
)
go

create table Models
(
    Id              int identity
        constraint PK_ThreeDModels
            primary key,
    Name            nvarchar(100),
    Summary         nvarchar(max),
    CreatedAt       datetime default getdate(),
    UpdatedAt       datetime default getdate(),
    IsEnabled       bit      default 1 not null,
    ModelCategoryId int      default 1 not null
        constraint FK_Models_ModelCategory
            references ModelCategories
)
go

create table OrderStatuses
(
    Id        int identity
        constraint PK_OrderStatuses
            primary key,
    Status    varchar(255)                  not null,
    IsEnabled bit                           not null,
    UpdatedAt datetime default getutcdate() not null
)
go

create table PaymentMethods
(
    Id               int identity
        constraint PK_PaymentMethods
            primary key,
    Name             nvarchar(255) not null,
    IsEnabled        bit           not null,
    LastModifiedDate datetime      not null,
    CreatedDate      datetime      not null
)
go

create table SellerSaleOrderPriorities
(
    Id           int identity
        constraint PK_SellerSaleOrderPriorities
            primary key,
    PriorityName nvarchar(50)
)
go

create table ShadeColors
(
    Id             int identity
        constraint PK_ShadeColors
            primary key,
    Name           nvarchar(100),
    Value          nvarchar(1000),
    GeneralColorId int
        references GeneralColors,
    IsActive       bit default 1
)
go

create table StoreSaleOrderAddresses
(
    Id           int identity
        constraint PK_StoreSaleOrderAddresses
            primary key,
    Address1     varchar(80) not null,
    Address2     varchar(80),
    City         varchar(40) not null,
    Country      varchar(40) not null,
    CountryCode  varchar(4),
    FirstName    varchar(40) not null,
    LastName     varchar(40) not null,
    Name         varchar(80) not null,
    Phone        varchar(50),
    Province     varchar(40) not null,
    ProvinceCode varchar(10),
    Zip          varchar(50) not null
)
go

create table StoreSaleOrderPriorities
(
    Id        int identity
        constraint PK_StoreSaleOrderPriorities
            primary key,
    Name      varchar(255)                  not null,
    IsEnabled bit      default 1            not null,
    UpdatedAt datetime default getutcdate() not null
)
go

create table UnitCategories
(
    Id          int identity
        constraint PK_UnitCategories
            primary key,
    Name        nvarchar(100),
    Description nvarchar(max)
)
go

create table Units
(
    Id          int identity
        constraint PK_Units
            primary key,
    CategoryId  int
        references UnitCategories,
    Name        nvarchar(100),
    Description nvarchar(max)
)
go

create table FilamentMaterials
(
    Id                int identity
        constraint PK_FilamentMaterials
            primary key,
    GeneralMaterialId int
        references FilamentGeneralMaterials,
    Name              nvarchar(100),
    Description       nvarchar(max),
    IsActive          bit            default 1,
    Density           decimal(18, 6) default 1  not null,
    DensityUnitId     int            default 22 not null
        constraint FK_FilamentMaterials_Units_DensityUnit
            references Units
)
go

create table Filaments
(
    Id                int identity
        constraint PK_Filaments
            primary key,
    Name              nvarchar(255)             not null,
    ColorId           int                       not null
        references ShadeColors,
    MaterialId        int                       not null
        references FilamentMaterials,
    BrandId           int                       not null
        references FilamentBrands,
    Description       nvarchar(255)             not null,
    Cost              decimal(18, 2)            not null,
    StockQuantity     int                       not null,
    CreatedAt         datetime       default getdate(),
    UpdatedAt         datetime       default getdate(),
    IsActive          bit            default 1,
    SpoolWeight       decimal(18, 6) default 1  not null,
    SpoolWeightUnitId int            default 10 not null
        constraint FK_Filaments_Units_SpoolWeightUnit
            references Units
)
go

create table ModelFiles
(
    Id                      int identity
        constraint PK_ThreeDModelFiles
            primary key,
    Name                    nvarchar(255)       not null,
    ThreeDModelId           int                 not null
        references Models,
    Uri                     nvarchar(255)       not null,
    Type                    nvarchar(255)       not null,
    Size                    bigint              not null,
    CreatedAt               datetime default getdate(),
    BlobName                nvarchar(255)       not null,
    IsVolumeDetermined      bit      default 0  not null,
    Volume                  decimal(18, 6),
    VolumeUnitId            int      default 13 not null
        constraint FK_ThreeDModelFiles_Units_Volume
            references Units,
    IsBoundingBoxDetermined bit      default 0  not null,
    BoundX                  decimal(18, 6),
    BoundY                  decimal(18, 6),
    BoundZ                  decimal(18, 6),
    IsEnabled               bit      default 1  not null
)
go

create table Users
(
    Id                      int identity
        constraint PK_Users
            primary key,
    RefId                   uniqueidentifier   not null,
    CreatedAt               datetime default getutcdate(),
    UpdatedAt               datetime default getutcdate(),
    IsEnabled               bit      default 1,
    Email                   nvarchar(255),
    Phone                   nvarchar(255),
    HasTakenTour            bit      default 0 not null,
    IsPrivacyPolicyAccepted bit      default 0 not null,
    FirstName               nvarchar(255),
    LastName                nvarchar(255)
)
go

create table Sellers
(
    Id             int identity
        constraint PK__Sellers
            primary key,
    Discount       decimal(5, 2),
    HasBeenUpdated bit,
    IsEnabled      bit                              not null,
    CreatedAt      datetime         default getutcdate(),
    UpdatedAt      datetime         default getutcdate(),
    Status         nvarchar(255),
    UserId         int              default 1       not null
        constraint FK_Sellers_Users
            references Users,
    UserRefId      uniqueidentifier default newid() not null
)
go

create table SellerAddresses
(
    Id               int identity
        constraint PK_SellerAddresses
            primary key,
    IsEnabled        bit                              not null,
    LastModifiedDate datetime                         not null,
    SellerId         int                              not null
        references Sellers,
    AddressId        int                              not null
        references Addresses,
    UserRefId        uniqueidentifier default newid() not null
)
go

create table SellerPaymentMethods
(
    Id               int identity
        constraint PK_SellerPaymentMethods
            primary key,
    IsDefault        bit                              not null,
    IsEnabled        bit                              not null,
    CreatedDate      datetime                         not null,
    LastModifiedDate datetime                         not null,
    SellerId         int                              not null
        references Sellers,
    PaymentMethodId  int                              not null
        references PaymentMethods,
    UserRefId        uniqueidentifier default newid() not null
)
go

create table BraintreeDetails
(
    Id                    int identity
        constraint PK_BraintreeDetails
            primary key,
    CardholderName        nvarchar(250) not null,
    ClientToken           nvarchar(max) not null,
    BraintreeSellerId     nvarchar(max) not null,
    Token                 nvarchar(max) not null,
    BillingAgreementId    nvarchar(max) not null,
    Type                  nvarchar(max) not null,
    PlaceHolder           nvarchar(max) not null,
    Tenant                nvarchar(max) not null,
    DeviceData            nvarchar(max) not null,
    IsEnabled             bit           not null,
    CreatedDate           datetime      not null,
    LastModifiedDate      datetime      not null,
    SellerPaymentMethodId int           not null
        references SellerPaymentMethods
)
go

create table PaypalDetails
(
    Id                    int identity
        constraint PK_PaypalDetails
            primary key,
    PayPalUserEmail       nvarchar(max) not null,
    Token                 nvarchar(max) not null,
    CorrelationId         nvarchar(max) not null,
    BillingAgreementId    nvarchar(max) not null,
    IsEnabled             bit           not null,
    CreatedDate           datetime      not null,
    LastModifiedDate      datetime      not null,
    SellerPaymentMethodId int           not null
        references SellerPaymentMethods
)
go

create table SellerPaymentTransactions
(
    Id                    int identity
        constraint PK_SellerPaymentTransactions
            primary key,
    Total                 decimal(19, 2)                   not null,
    IsEnabled             bit                              not null,
    CreatedAt             datetime                         not null,
    UpdatedAt             datetime                         not null,
    SellerPaymentMethodId int                              not null
        references SellerPaymentMethods,
    UserRefId             uniqueidentifier default newid() not null
)
go

create table BraintreeTransactionDetails
(
    Id                         int identity
        constraint PK_BraintreeTransactionDetails
            primary key,
    BraintreeTransactionId     nvarchar(max) not null,
    SellerPaymentTransactionId int           not null
        references SellerPaymentTransactions
)
go

create table PayPalTransactionDetails
(
    Id                         int identity
        constraint PK_PayPalTransactionDetails
            primary key,
    PayPalCorrelationId        nvarchar(max) not null,
    PayPalTransactionId        nvarchar(max) not null,
    SellerPaymentTransactionId int           not null
        references SellerPaymentTransactions
)
go

create table SellerProducts
(
    Id          int identity
        constraint PK__SellerProducts
            primary key,
    IsEnabled   bit                              not null,
    CreatedAt   datetime         default getutcdate(),
    UpdatedAt   datetime         default getutcdate(),
    SellerId    int                              not null
        references Sellers,
    Tags        nvarchar(max),
    Title       nvarchar(255),
    Type        nvarchar(255),
    Description nvarchar(max),
    BodyHtml    nvarchar(max),
    Url         nvarchar(max),
    UserRefId   uniqueidentifier default newid() not null
)
go

create table SellerProductVariants
(
    IsEnabled        bit           not null,
    Price            decimal(5, 2),
    CreatedDate      datetime      not null,
    LastModifiedDate datetime      not null,
    SellerProductId  int           not null
        references SellerProducts,
    Name             nvarchar(255),
    Tags             nvarchar(max),
    Color            nvarchar(255),
    Size             nvarchar(255),
    Material         nvarchar(255),
    ShippingPrice    decimal(5, 2),
    Weight           decimal(5, 2),
    Id               int identity
        constraint PK_SellerProductVariants
            primary key,
    WeightUnitId     int default 9 not null
        constraint FK_SellerProductVariants_Units
            references Units
)
go

create table ProductPieces
(
    Id                     int identity
        constraint PK_ProductPieces
            primary key,
    IsEnabled              bit      default 1,
    CreatedAt              datetime default getutcdate() not null,
    UpdatedAt              datetime default getutcdate() not null,
    FilamentId             int                           not null
        references Filaments,
    ThreeDModelFileId      int                           not null
        references ModelFiles,
    SellerProductVariantId int      default 0            not null
        constraint FK_ProductPieces_SellerProductVariants
            references SellerProductVariants
)
go

create table SellerProductVariantImages
(
    Id                     int identity
        constraint PK_SellerProductVariantImages
            primary key,
    Url                    nvarchar(255)                 not null,
    IsEnabled              bit      default 1,
    CreatedAt              datetime default getutcdate() not null,
    UpdatedAt              datetime default getutcdate() not null,
    ImageTypeId            int                           not null
        references ImageTypes,
    Name                   nvarchar(255),
    Alt                    nvarchar(max),
    SellerProductVariantId int      default (-1)         not null
        constraint FK_SellerProductVariantImages_SellerProductVariants
            references SellerProductVariants,
    IsDefaultImage         bit      default 0            not null
)
go

create table StoreSaleTransactions
(
    Id                         int identity
        constraint PK_StoreSaleTransactions
            primary key,
    IsEnabled                  bit            not null,
    UpdatedAt                  datetime       not null,
    Price                      decimal(19, 2) not null,
    Discount                   decimal(19, 2) not null,
    Tax                        decimal(19, 2) not null,
    Total                      decimal(19, 2) not null,
    SellerPaymentTransactionId int            not null
        references SellerPaymentTransactions
)
go

create table Stores
(
    Id                    int identity
        constraint PK_Shops
            primary key,
    Name                  nvarchar(max)                    not null,
    Token                 nvarchar(max)                    not null,
    RefreshToken          nvarchar(max),
    TokenExpireDate       datetime,
    IsEnabled             bit                              not null,
    LastModifiedDate      datetime                         not null,
    LastSyncDate          datetime,
    ShopIdentifier        nvarchar(max)                    not null,
    IsTokenRevoked        bit                              not null,
    MarketPlaceId         int                              not null
        references MarketPlaces,
    SellerId              int                              not null
        references Sellers,
    IsShopifyScopeUpdated bit,
    Status                nvarchar(255),
    UserRefId             uniqueidentifier default newid() not null
)
go

create table StoreProducts
(
    Id                             int identity
        constraint PK__StoreProducts
            primary key,
    IsEnabled                      bit not null,
    CreatedAt                      datetime default getutcdate(),
    UpdatedAt                      datetime default getutcdate(),
    PublishingStatus               int not null,
    LastPublishingStatusChangeDate datetime,
    PublishRetryCount              int,
    StoreId                        int not null
        references Stores,
    Tags                           nvarchar(max),
    Title                          nvarchar(255),
    Type                           nvarchar(255),
    Description                    nvarchar(max),
    BodyHtml                       nvarchar(max),
    Url                            nvarchar(max),
    ProductOnlineStoreId           nvarchar(max)
)
go

create table StoreProductVariants
(
    Id                          int identity
        constraint PK__StoreProductVariants
            primary key,
    IsEnabled                   bit                         not null,
    CreatedAt                   datetime      default getutcdate(),
    UpdatedAt                   datetime      default getutcdate(),
    StoreProductId              int                         not null
        references StoreProducts,
    Name                        nvarchar(255),
    Tags                        nvarchar(max),
    Color                       nvarchar(255),
    Size                        nvarchar(255),
    Material                    nvarchar(255),
    ShippingPrice               decimal(5, 2),
    Weight                      decimal(5, 2),
    Price                       decimal(5, 2),
    SellerProductVariantId      int           default (-1)  not null
        constraint FK_StoreProductVariants_SellerProductVariants
            references SellerProductVariants,
    WeightUnitId                int           default 9     not null
        constraint FK_StoreProductVariants_Units
            references Units,
    OnlineStoreProductVariantId nvarchar(max),
    SKU                         nvarchar(max) default 'SKU' not null
)
go

create table StoreProductVariantImages
(
    Id                    int identity
        constraint PK_StoreProductVariantImages
            primary key,
    IsEnabled             bit           not null,
    CreatedDate           datetime      not null,
    LastModifiedDate      datetime      not null,
    Name                  nvarchar(255),
    Url                   nvarchar(max),
    Alt                   nvarchar(max),
    ImageTypeId           int           not null
        references ImageTypes,
    StoreProductVariantId int           not null
        references StoreProductVariants,
    IsDefaultImage        bit default 0 not null
)
go

create table StoreSaleOrders
(
    Id                       int identity
        constraint PK_StoreSaleOrders
            primary key,
    IsEnabled                bit      default 1            not null,
    CreatedAt                datetime default getutcdate() not null,
    UpdatedAt                datetime default getutcdate() not null,
    ShippingLabelId          nvarchar(max),
    TotalCost                decimal(19)                   not null,
    OrderNumber              nvarchar(255),
    StoreOrderIdentifier     nvarchar(max),
    StoreOrderNumber         nvarchar(max),
    TrackingNumber           nvarchar(150),
    ContactEmail             varchar(255),
    StoreId                  int                           not null
        references Stores,
    StoreSaleOrderPriorityId int                           not null
        references StoreSaleOrderPriorities,
    StoreSaleOrderAddressId  int                           not null
        references StoreSaleOrderAddresses
)
go

create table OrderNotes
(
    Id               int identity
        constraint PK_OrderNotes
            primary key,
    Note             nvarchar(255)                 not null,
    UpdatedAt        datetime default getutcdate() not null,
    StoreSaleOrderId int                           not null
        references StoreSaleOrders
)
go

create table StoreSaleOrderDetails
(
    Id                    int identity
        constraint PK_StoreSaleOrderDetails
            primary key,
    IsEnabled             bit                           not null,
    UpdatedAt             datetime default getutcdate() not null,
    Quantity              int                           not null,
    OrderItemNumber       nvarchar(255),
    StorePrice            decimal(19, 2),
    StoreProductId        nvarchar(255),
    StoreOrderLineItemId  nvarchar(255),
    StoreVariantTitle     nvarchar(255),
    ItemPrice             decimal(19, 2)                not null,
    ShippingPrice         decimal(19, 2)                not null,
    Discount              decimal(19, 2)                not null,
    Total                 decimal(19, 2)                not null,
    StoreSaleOrderId      int                           not null
        references StoreSaleOrders,
    StoreProductVariantId int                           not null
        references StoreProductVariants
)
go

create table StoreSaleOrderStatuses
(
    Id               int identity
        constraint PK_CustomerSaleOrderStatuses
            primary key,
    IsEnabled        bit                           not null,
    UpdatedAt        datetime default getutcdate() not null,
    ProcessCount     int                           not null,
    OrderStatusId    int                           not null
        references OrderStatuses,
    StoreSaleOrderId int      default 1            not null
        constraint FK_StoreSaleOrderStatuses_StoreSaleOrders
            references StoreSaleOrders
)
go

create table StoreSaleTransactionDetails
(
    Id                     int identity
        constraint PK_StoreSaleTransactionDetails
            primary key,
    IsEnabled              bit            not null,
    UpdatedAt              datetime       not null,
    Price                  decimal(19, 2) not null,
    Discount               decimal(19, 2) not null,
    Tax                    decimal(19, 2) not null,
    Total                  decimal(19, 2) not null,
    StoreSaleTransactionId int            not null
        references StoreSaleTransactions,
    StoreSaleOrderDetailId int            not null
        references StoreSaleOrderDetails
)
go

create table StripeDetails
(
    Id                    int identity
        constraint PK_StripeDetails
            primary key,
    StripeSellerId        nvarchar(max) not null,
    StripeSetupIntentId   nvarchar(max) not null,
    StripePaymentMethodId nvarchar(max) not null,
    PlaceHolder           nvarchar(max) not null,
    CardholderName        nvarchar(250) not null,
    ExpireDate            nvarchar(250) not null,
    IsEnabled             bit           not null,
    CreatedDate           datetime      not null,
    LastModifiedDate      datetime      not null,
    SellerPaymentMethodId int           not null
        references SellerPaymentMethods
)
go

create table StripeTransactionDetails
(
    Id                         int identity
        constraint PK_StripeTransactionDetails
            primary key,
    StripePaymentId            nvarchar(max) not null,
    SellerPaymentTransactionId int           not null
        references SellerPaymentTransactions
)
go

create table VersionInfo
(
    Version     bigint not null,
    AppliedOn   datetime,
    Description nvarchar(1024)
)
go

create unique clustered index UC_Version
    on VersionInfo (Version)
go

