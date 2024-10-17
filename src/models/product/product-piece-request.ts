interface IProductPieceRequest {
    filament_id: number;
    model_file_id: number;
}

class ProductPieceRequest implements IProductPieceRequest {
    constructor(
        public filament_id: number,
        public model_file_id: number
    ) {}

    static fromJSON(data: any): ProductPieceRequest {
        return new ProductPieceRequest(
            data.filament_id,
            data.model_file_id
        );
    }
}
