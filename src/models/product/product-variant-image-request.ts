interface IProductVariantImageRequest {
    id: number;
    name?: string;
    alt?: string;
    is_default_image: boolean;
}

class ProductVariantImageRequest implements IProductVariantImageRequest {
    constructor(
        public id: number,
        public is_default_image: boolean,
        public name?: string,
        public alt?: string
    ) {}

    static fromJSON(data: any): ProductVariantImageRequest {
        return new ProductVariantImageRequest(
            data.id,
            data.is_default_image,
            data.name,
            data.alt
        );
    }
}
