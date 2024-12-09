interface IProductImage {
    id: number;
    url: string;
    created_at: Date;
    updated_at: Date;
    image_type_id: number;
    name?: string;
    alt?: string;
    seller_product_variant_id: number;
    is_default_image: boolean;
}

class ProductImage implements IProductImage{
    constructor(
        public id: number,
        public url: string,
        public created_at: Date,
        public updated_at: Date,
        public image_type_id: number,
        public seller_product_variant_id: number,
        public is_default_image: boolean,
        public name?: string,
        public alt?: string
    ) {}

    static fromJSON(data: any): ProductImage {
        return new ProductImage(
            data.id,
            data.url,
            new Date(data.created_at),
            new Date(data.updated_at),
            data.image_type_id,
            data.seller_product_variant_id,
            data.is_default_image,
            data.name,
            data.alt
        );
    }
}
