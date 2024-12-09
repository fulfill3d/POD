interface IProductVariantRequest {
    id?: number;
    product_id: number;
    name?: string;
    tags?: string;
    pieces?: IProductPieceRequest[];
    images?: IProductVariantImageRequest[];
}

class ProductVariantRequest implements IProductVariantRequest {
    constructor(
        public product_id: number,
        public id?: number,
        public name?: string,
        public tags?: string,
        public pieces?: IProductPieceRequest[],
        public images?: IProductVariantImageRequest[]
    ) {}

    static fromJSON(data: any): ProductVariantRequest {
        return new ProductVariantRequest(
            data.product_id,
            data.id,
            data.name,
            data.tags,
            data.pieces?.map((p: any) => ProductPieceRequest.fromJSON(p)) || [],
            data.images?.map((i: any) => ProductVariantImageRequest.fromJSON(i)) || []
        );
    }
}
