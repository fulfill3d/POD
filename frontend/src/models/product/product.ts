interface IProduct {
    id: number;
    description?: string;
    created_at?: Date;
    updated_at?: Date;
    url?: string;
    tags?: string;
    title?: string;
    type?: string;
    body_html?: string;
    product_variant_views: IProductVariant[];
}

class Product implements IProduct {
    constructor(
        public id: number,
        public description?: string,
        public created_at?: Date,
        public updated_at?: Date,
        public url?: string,
        public tags?: string,
        public title?: string,
        public type?: string,
        public body_html?: string,
        public product_variant_views: IProductVariant[] = []
    ) {}

    static fromJSON(data: any): Product {
        return new Product(
            data.id,
            data.description,
            data.created_at ? new Date(data.created_at) : undefined,
            data.updated_at ? new Date(data.updated_at) : undefined,
            data.url,
            data.tags,
            data.title,
            data.type,
            data.body_html,
            data.product_variant_views?.map((v: any) => ProductVariant.fromJSON(v)) || []
        );
    }
}
