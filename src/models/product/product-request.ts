interface IProductRequest {
    id?: number;
    title?: string;
    description?: string;
    body_html?: string;
    tags?: string;
    type?: string;
    variants?: IProductVariantRequest[];
}

class ProductRequest implements IProductRequest {
    constructor(
        public id?: number,
        public title?: string,
        public description?: string,
        public body_html?: string,
        public tags?: string,
        public type?: string,
        public variants?: IProductVariantRequest[]
    ) {}

    static fromJSON(data: any): ProductRequest {
        return new ProductRequest(
            data.id,
            data.title,
            data.description,
            data.body_html,
            data.tags,
            data.type,
            data.variants?.map((v: any) => ProductVariantRequest.fromJSON(v)) || []
        );
    }
}
