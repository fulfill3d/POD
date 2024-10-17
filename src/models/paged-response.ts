interface IPagedResponse<T> {
    page_number: number;
    page_size: number;
    total_count: number;
    data: T[];
}

class PagedResponse<T> implements IPagedResponse<T> {
    constructor(
        public page_number: number,
        public page_size: number,
        public total_count: number,
        public data: T[]
    ) {}

    static fromJSON<T>(data: any, mapItem: (item: any) => T): PagedResponse<T> {
        return new PagedResponse<T>(
            data.page_number,
            data.page_size,
            data.total_count,
            data.data.map((item: any) => mapItem(item))
        );
    }
}
