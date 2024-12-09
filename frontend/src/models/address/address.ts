interface Address {
    id: number;
    seller_address_id: number;
    first_name: string;
    last_name: string;
    street1: string;
    street2: string;
    city: string;
    state: string;
    country: string;
    zip_code: string;
}

class AddressModel implements Address {
    constructor(
        public id: number,
        public seller_address_id: number,
        public first_name: string,
        public last_name: string,
        public street1: string,
        public street2: string,
        public city: string,
        public state: string,
        public country: string,
        public zip_code: string
    ) {}

    static fromJSON(data: any): AddressModel {
        return new AddressModel(
            data.id,
            data.seller_address_id,
            data.first_name,
            data.last_name,
            data.street1,
            data.street2,
            data.city,
            data.state,
            data.country,
            data.zip_code
        );
    }

    validate(): string[] {
        const errors: string[] = [];

        if (!this.first_name.trim()) {
            errors.push("FirstName is required");
        } else if (this.first_name.trim().length > 40) {
            errors.push("FirstName must be less than 40 characters");
        }

        if (!this.last_name.trim()) {
            errors.push("LastName is required");
        } else if (this.last_name.trim().length > 40) {
            errors.push("LastName must be less than 40 characters");
        }

        if (!this.street1.trim()) {
            errors.push("Street1 is required");
        }

        if (!this.city.trim()) {
            errors.push("City is required");
        }

        if (!this.state.trim()) {
            errors.push("State is required");
        } else if (this.state.trim().length !== 2) {
            errors.push("State must be 2 characters");
        }

        if (!this.country.trim()) {
            errors.push("Country is required");
        } else if (this.country.trim().length !== 2) {
            errors.push("Country must be 2 characters");
        }

        if (!this.zip_code.trim()) {
            errors.push("ZipCode is required");
        }

        return errors;
    }
}
