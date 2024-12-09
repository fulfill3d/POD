interface IProductPiece {
    id: number;
    product_piece_created_at: Date;
    product_piece_updated_at: Date;
    filament_name: string;
    filament_brand: string;
    filament_description: string;
    filament_color: string;
    filament_material: string;
    filament_material_description: string;
    model_name: string;
    model_uri: string;
    model_type: string;
    model_size: number;
    model_created_at?: Date;
    model_volume?: number;
    volume_unit_id: number;
}

class ProductPiece implements IProductPiece {
    constructor(
        public id: number,
        public product_piece_created_at: Date,
        public product_piece_updated_at: Date,
        public filament_name: string,
        public filament_brand: string,
        public filament_description: string,
        public filament_color: string,
        public filament_material: string,
        public filament_material_description: string,
        public model_name: string,
        public model_uri: string,
        public model_type: string,
        public model_size: number,
        public volume_unit_id: number,
        public model_created_at?: Date,
        public model_volume?: number
    ) {}

    static fromJSON(data: any): ProductPiece {
        return new ProductPiece(
            data.id,
            new Date(data.product_piece_created_at),
            new Date(data.product_piece_updated_at),
            data.filament_name,
            data.filament_brand,
            data.filament_description,
            data.filament_color,
            data.filament_material,
            data.filament_material_description,
            data.model_name,
            data.model_uri,
            data.model_type,
            data.model_size,
            data.volume_unit_id,
            data.model_created_at ? new Date(data.model_created_at) : undefined,
            data.model_volume
        );
    }
}
