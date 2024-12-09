interface IProductVariant {
    id: number;
    price?: number;
    created_date: Date;
    last_modified_date: Date;
    seller_product_id: number;
    name?: string;
    tags?: string;
    color?: string;
    size?: string;
    material?: string;
    shipping_price?: number;
    weight?: number;
    weight_unit_id: number;
    product_image_views: IProductImage[];
    product_piece_views: IProductPieceRequest[];
}

class ProductVariant implements IProductVariant{
    constructor(
        public id: number,
        public created_date: Date,
        public last_modified_date: Date,
        public seller_product_id: number,
        public weight_unit_id: number,
        public product_image_views: IProductImage[],
        public product_piece_views: IProductPieceRequest[],
        public price?: number,
        public name?: string,
        public tags?: string,
        public color?: string,
        public size?: string,
        public material?: string,
        public shipping_price?: number,
        public weight?: number
    ) {}

    static fromJSON(data: any): ProductVariant {
        return new ProductVariant(
            data.id,
            new Date(data.created_date),
            new Date(data.last_modified_date),
            data.seller_product_id,
            data.weight_unit_id,
            data.product_image_views?.map((v: any) => ProductImage.fromJSON(v)) || [],
            data.product_piece_views?.map((v: any) => ProductPiece.fromJSON(v)) || [],
            data.price,
            data.name,
            data.tags,
            data.color,
            data.size,
            data.material,
            data.shipping_price,
            data.weight
        );
    }
}
