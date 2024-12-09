using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace POD.Common.Database.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<BlockedUser> BlockedUsers { get; set; } = null!;
        public virtual DbSet<BraintreeDetail> BraintreeDetails { get; set; } = null!;
        public virtual DbSet<BraintreeTransactionDetail> BraintreeTransactionDetails { get; set; } = null!;
        public virtual DbSet<Configuration> Configurations { get; set; } = null!;
        public virtual DbSet<Filament> Filaments { get; set; } = null!;
        public virtual DbSet<FilamentBrand> FilamentBrands { get; set; } = null!;
        public virtual DbSet<FilamentGeneralMaterial> FilamentGeneralMaterials { get; set; } = null!;
        public virtual DbSet<FilamentMaterial> FilamentMaterials { get; set; } = null!;
        public virtual DbSet<GeneralColor> GeneralColors { get; set; } = null!;
        public virtual DbSet<ImageType> ImageTypes { get; set; } = null!;
        public virtual DbSet<MarketPlace> MarketPlaces { get; set; } = null!;
        public virtual DbSet<Model> Models { get; set; } = null!;
        public virtual DbSet<ModelCategory> ModelCategories { get; set; } = null!;
        public virtual DbSet<ModelFile> ModelFiles { get; set; } = null!;
        public virtual DbSet<OrderNote> OrderNotes { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<PayPalTransactionDetail> PayPalTransactionDetails { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<PaypalDetail> PaypalDetails { get; set; } = null!;
        public virtual DbSet<ProductPiece> ProductPieces { get; set; } = null!;
        public virtual DbSet<Seller> Sellers { get; set; } = null!;
        public virtual DbSet<SellerAddress> SellerAddresses { get; set; } = null!;
        public virtual DbSet<SellerPaymentMethod> SellerPaymentMethods { get; set; } = null!;
        public virtual DbSet<SellerPaymentTransaction> SellerPaymentTransactions { get; set; } = null!;
        public virtual DbSet<SellerProduct> SellerProducts { get; set; } = null!;
        public virtual DbSet<SellerProductVariant> SellerProductVariants { get; set; } = null!;
        public virtual DbSet<SellerProductVariantImage> SellerProductVariantImages { get; set; } = null!;
        public virtual DbSet<SellerSaleOrderPriority> SellerSaleOrderPriorities { get; set; } = null!;
        public virtual DbSet<ShadeColor> ShadeColors { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreProduct> StoreProducts { get; set; } = null!;
        public virtual DbSet<StoreProductVariant> StoreProductVariants { get; set; } = null!;
        public virtual DbSet<StoreProductVariantImage> StoreProductVariantImages { get; set; } = null!;
        public virtual DbSet<StoreSaleOrder> StoreSaleOrders { get; set; } = null!;
        public virtual DbSet<StoreSaleOrderAddress> StoreSaleOrderAddresses { get; set; } = null!;
        public virtual DbSet<StoreSaleOrderDetail> StoreSaleOrderDetails { get; set; } = null!;
        public virtual DbSet<StoreSaleOrderPriority> StoreSaleOrderPriorities { get; set; } = null!;
        public virtual DbSet<StoreSaleOrderStatus> StoreSaleOrderStatuses { get; set; } = null!;
        public virtual DbSet<StoreSaleTransaction> StoreSaleTransactions { get; set; } = null!;
        public virtual DbSet<StoreSaleTransactionDetail> StoreSaleTransactionDetails { get; set; } = null!;
        public virtual DbSet<StripeDetail> StripeDetails { get; set; } = null!;
        public virtual DbSet<StripeTransactionDetail> StripeTransactionDetails { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<UnitCategory> UnitCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<VersionInfo> VersionInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Street1)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Street2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BlockedUser>(entity =>
            {
                entity.ToTable("BlockedUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Shop)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BraintreeDetail>(entity =>
            {
                entity.Property(e => e.CardholderName).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SellerPaymentMethod)
                    .WithMany(p => p.BraintreeDetails)
                    .HasForeignKey(d => d.SellerPaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Braintree__Selle__2F4FF79D");
            });

            modelBuilder.Entity<BraintreeTransactionDetail>(entity =>
            {
                entity.HasOne(d => d.SellerPaymentTransaction)
                    .WithMany(p => p.BraintreeTransactionDetails)
                    .HasForeignKey(d => d.SellerPaymentTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Braintree__Selle__6324A15E");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.ToTable("Configuration");

                entity.Property(e => e.ConfigurationId).ValueGeneratedNever();

                entity.Property(e => e.Configuration1).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Filament>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.SpoolWeight)
                    .HasColumnType("decimal(18, 6)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SpoolWeightUnitId).HasDefaultValueSql("((10))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Filaments)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Filaments__Brand__041093DD");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Filaments)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Filaments__Color__02284B6B");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Filaments)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Filaments__Mater__031C6FA4");

                entity.HasOne(d => d.SpoolWeightUnit)
                    .WithMany(p => p.Filaments)
                    .HasForeignKey(d => d.SpoolWeightUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Filaments_Units_SpoolWeightUnit");
            });

            modelBuilder.Entity<FilamentBrand>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Origin).HasMaxLength(100);
            });

            modelBuilder.Entity<FilamentGeneralMaterial>(entity =>
            {
                entity.Property(e => e.BedTemperature).HasMaxLength(100);

                entity.Property(e => e.HeatBed).HasMaxLength(100);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NozzleTemperature).HasMaxLength(100);
            });

            modelBuilder.Entity<FilamentMaterial>(entity =>
            {
                entity.Property(e => e.Density)
                    .HasColumnType("decimal(18, 6)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DensityUnitId).HasDefaultValueSql("((22))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.DensityUnit)
                    .WithMany(p => p.FilamentMaterials)
                    .HasForeignKey(d => d.DensityUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FilamentMaterials_Units_DensityUnit");

                entity.HasOne(d => d.GeneralMaterial)
                    .WithMany(p => p.FilamentMaterials)
                    .HasForeignKey(d => d.GeneralMaterialId)
                    .HasConstraintName("FK__FilamentM__Gener__5832119F");
            });

            modelBuilder.Entity<GeneralColor>(entity =>
            {
                entity.Property(e => e.Hex).HasMaxLength(10);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ImageType>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<MarketPlace>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModelCategoryId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ModelCategory)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.ModelCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Models_ModelCategory");
            });

            modelBuilder.Entity<ModelCategory>(entity =>
            {
                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.ParentNavigation)
                    .WithMany(p => p.InverseParentNavigation)
                    .HasForeignKey(d => d.Parent)
                    .HasConstraintName("FK_ThreeDModelCategories_Parent");
            });

            modelBuilder.Entity<ModelFile>(entity =>
            {
                entity.Property(e => e.BlobName).HasMaxLength(255);

                entity.Property(e => e.BoundX).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.BoundY).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.BoundZ).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ThreeDmodelId).HasColumnName("ThreeDModelId");

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Uri).HasMaxLength(255);

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.VolumeUnitId).HasDefaultValueSql("((13))");

                entity.HasOne(d => d.ThreeDmodel)
                    .WithMany(p => p.ModelFiles)
                    .HasForeignKey(d => d.ThreeDmodelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThreeDMod__Three__70FDBF69");

                entity.HasOne(d => d.VolumeUnit)
                    .WithMany(p => p.ModelFiles)
                    .HasForeignKey(d => d.VolumeUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ThreeDModelFiles_Units_Volume");
            });

            modelBuilder.Entity<OrderNote>(entity =>
            {
                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.StoreSaleOrder)
                    .WithMany(p => p.OrderNotes)
                    .HasForeignKey(d => d.StoreSaleOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderNote__Store__224B023A");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<PayPalTransactionDetail>(entity =>
            {
                entity.HasOne(d => d.SellerPaymentTransaction)
                    .WithMany(p => p.PayPalTransactionDetails)
                    .HasForeignKey(d => d.SellerPaymentTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PayPalTra__Selle__66010E09");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<PaypalDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SellerPaymentMethod)
                    .WithMany(p => p.PaypalDetails)
                    .HasForeignKey(d => d.SellerPaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PaypalDet__Selle__3508D0F3");
            });

            modelBuilder.Entity<ProductPiece>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.ThreeDmodelFileId).HasColumnName("ThreeDModelFileId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Filament)
                    .WithMany(p => p.ProductPieces)
                    .HasForeignKey(d => d.FilamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductPi__Filam__216BEC9A");

                entity.HasOne(d => d.SellerProductVariant)
                    .WithMany(p => p.ProductPieces)
                    .HasForeignKey(d => d.SellerProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductPieces_SellerProductVariants");

                entity.HasOne(d => d.ThreeDmodelFile)
                    .WithMany(p => p.ProductPieces)
                    .HasForeignKey(d => d.ThreeDmodelFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductPi__Three__226010D3");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserRefId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sellers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sellers_Users");
            });

            modelBuilder.Entity<SellerAddress>(entity =>
            {
                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRefId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.SellerAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerAdd__Addre__3E923B2D");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerAddresses)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerAdd__Selle__3D9E16F4");
            });

            modelBuilder.Entity<SellerPaymentMethod>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRefId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.SellerPaymentMethods)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerPay__Payme__2C738AF2");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerPaymentMethods)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerPay__Selle__2B7F66B9");
            });

            modelBuilder.Entity<SellerPaymentTransaction>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserRefId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.SellerPaymentMethod)
                    .WithMany(p => p.SellerPaymentTransactions)
                    .HasForeignKey(d => d.SellerPaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerPay__Selle__604834B3");
            });

            modelBuilder.Entity<SellerProduct>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.UserRefId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.SellerProducts)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerPro__Selle__1995C0A8");
            });

            modelBuilder.Entity<SellerProductVariant>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Material).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ShippingPrice).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Size).HasMaxLength(255);

                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.WeightUnitId).HasDefaultValueSql("((9))");

                entity.HasOne(d => d.SellerProduct)
                    .WithMany(p => p.SellerProductVariants)
                    .HasForeignKey(d => d.SellerProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerPro__Selle__08F5448B");

                entity.HasOne(d => d.WeightUnit)
                    .WithMany(p => p.SellerProductVariants)
                    .HasForeignKey(d => d.WeightUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellerProductVariants_Units");
            });

            modelBuilder.Entity<SellerProductVariantImage>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.SellerProductVariantId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.ImageType)
                    .WithMany(p => p.SellerProductVariantImages)
                    .HasForeignKey(d => d.ImageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SellerPro__Image__71DCD509");

                entity.HasOne(d => d.SellerProductVariant)
                    .WithMany(p => p.SellerProductVariantImages)
                    .HasForeignKey(d => d.SellerProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellerProductVariantImages_SellerProductVariants");
            });

            modelBuilder.Entity<SellerSaleOrderPriority>(entity =>
            {
                entity.Property(e => e.PriorityName).HasMaxLength(50);
            });

            modelBuilder.Entity<ShadeColor>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(1000);

                entity.HasOne(d => d.GeneralColor)
                    .WithMany(p => p.ShadeColors)
                    .HasForeignKey(d => d.GeneralColorId)
                    .HasConstraintName("FK__ShadeColo__Gener__51851410");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastSyncDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(255);

                entity.Property(e => e.TokenExpireDate).HasColumnType("datetime");

                entity.Property(e => e.UserRefId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.MarketPlace)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.MarketPlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shops__MarketPla__37E53D9E");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Shops__SellerId__38D961D7");
            });

            modelBuilder.Entity<StoreProduct>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastPublishingStatusChangeDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreProd__Store__5A054B78");
            });

            modelBuilder.Entity<StoreProductVariant>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Material).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.SellerProductVariantId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.ShippingPrice).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Size).HasMaxLength(255);

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasDefaultValueSql("('SKU')");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.WeightUnitId).HasDefaultValueSql("((9))");

                entity.HasOne(d => d.SellerProductVariant)
                    .WithMany(p => p.StoreProductVariants)
                    .HasForeignKey(d => d.SellerProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProductVariants_SellerProductVariants");

                entity.HasOne(d => d.StoreProduct)
                    .WithMany(p => p.StoreProductVariants)
                    .HasForeignKey(d => d.StoreProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreProd__Store__5ECA0095");

                entity.HasOne(d => d.WeightUnit)
                    .WithMany(p => p.StoreProductVariants)
                    .HasForeignKey(d => d.WeightUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProductVariants_Units");
            });

            modelBuilder.Entity<StoreProductVariantImage>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.ImageType)
                    .WithMany(p => p.StoreProductVariantImages)
                    .HasForeignKey(d => d.ImageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreProd__Image__133DC8D4");

                entity.HasOne(d => d.StoreProductVariant)
                    .WithMany(p => p.StoreProductVariantImages)
                    .HasForeignKey(d => d.StoreProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreProd__Store__1431ED0D");
            });

            modelBuilder.Entity<StoreSaleOrder>(entity =>
            {
                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderNumber).HasMaxLength(255);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(19, 0)");

                entity.Property(e => e.TrackingNumber).HasMaxLength(150);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreSaleOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__1C9228E4");

                entity.HasOne(d => d.StoreSaleOrderAddress)
                    .WithMany(p => p.StoreSaleOrders)
                    .HasForeignKey(d => d.StoreSaleOrderAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__1E7A7156");

                entity.HasOne(d => d.StoreSaleOrderPriority)
                    .WithMany(p => p.StoreSaleOrders)
                    .HasForeignKey(d => d.StoreSaleOrderPriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__1D864D1D");
            });

            modelBuilder.Entity<StoreSaleOrderAddress>(entity =>
            {
                entity.Property(e => e.Address1)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StoreSaleOrderDetail>(entity =>
            {
                entity.Property(e => e.Discount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.ItemPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.OrderItemNumber).HasMaxLength(255);

                entity.Property(e => e.ShippingPrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.StoreOrderLineItemId).HasMaxLength(255);

                entity.Property(e => e.StorePrice).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.StoreProductId).HasMaxLength(255);

                entity.Property(e => e.StoreVariantTitle).HasMaxLength(255);

                entity.Property(e => e.Total).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.StoreProductVariant)
                    .WithMany(p => p.StoreSaleOrderDetails)
                    .HasForeignKey(d => d.StoreProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__2EB0D91F");

                entity.HasOne(d => d.StoreSaleOrder)
                    .WithMany(p => p.StoreSaleOrderDetails)
                    .HasForeignKey(d => d.StoreSaleOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__2DBCB4E6");
            });

            modelBuilder.Entity<StoreSaleOrderPriority>(entity =>
            {
                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<StoreSaleOrderStatus>(entity =>
            {
                entity.Property(e => e.StoreSaleOrderId).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.StoreSaleOrderStatuses)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Order__28F7FFC9");

                entity.HasOne(d => d.StoreSaleOrder)
                    .WithMany(p => p.StoreSaleOrderStatuses)
                    .HasForeignKey(d => d.StoreSaleOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreSaleOrderStatuses_StoreSaleOrders");
            });

            modelBuilder.Entity<StoreSaleTransaction>(entity =>
            {
                entity.Property(e => e.Discount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Tax).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.SellerPaymentTransaction)
                    .WithMany(p => p.StoreSaleTransactions)
                    .HasForeignKey(d => d.SellerPaymentTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Selle__6BB9E75F");
            });

            modelBuilder.Entity<StoreSaleTransactionDetail>(entity =>
            {
                entity.Property(e => e.Discount).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Tax).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.StoreSaleOrderDetail)
                    .WithMany(p => p.StoreSaleTransactionDetails)
                    .HasForeignKey(d => d.StoreSaleOrderDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__6F8A7843");

                entity.HasOne(d => d.StoreSaleTransaction)
                    .WithMany(p => p.StoreSaleTransactionDetails)
                    .HasForeignKey(d => d.StoreSaleTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreSale__Store__6E96540A");
            });

            modelBuilder.Entity<StripeDetail>(entity =>
            {
                entity.Property(e => e.CardholderName).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasMaxLength(250);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SellerPaymentMethod)
                    .WithMany(p => p.StripeDetails)
                    .HasForeignKey(d => d.SellerPaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StripeDet__Selle__322C6448");
            });

            modelBuilder.Entity<StripeTransactionDetail>(entity =>
            {
                entity.HasOne(d => d.SellerPaymentTransaction)
                    .WithMany(p => p.StripeTransactionDetails)
                    .HasForeignKey(d => d.SellerPaymentTransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StripeTra__Selle__68DD7AB4");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Units__CategoryI__0D99FE17");
            });

            modelBuilder.Entity<UnitCategory>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<VersionInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VersionInfo");

                entity.HasIndex(e => e.Version, "UC_Version")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.AppliedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1024);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
