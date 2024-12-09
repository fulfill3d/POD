import React, { useState } from 'react';
import Image from 'next/image';

interface ImageWithLoaderProps {
    src: string;
    alt: string;
    className?: string;
    placeholderSrc?: string;
}

const ImageWithLoader: React.FC<ImageWithLoaderProps> = ({
                                                             src,
                                                             alt,
                                                             className = '',
                                                             placeholderSrc,
                                                         }) => {
    const [isLoading, setIsLoading] = useState(true);
    const [hasError, setHasError] = useState(false);

    const handleLoad = () => {
        setIsLoading(false);
    };

    const handleError = () => {
        setHasError(true);
        setIsLoading(false);
    };

    return (
        <div className={`relative w-full h-full ${className}`}>
            {isLoading && (
                <div className="absolute inset-0 flex items-center justify-center">
                    <svg
                        className="animate-spin h-10 w-10 text-coral"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                    >
                        <circle
                            className="opacity-25"
                            cx="12"
                            cy="12"
                            r="10"
                            stroke="currentColor"
                            strokeWidth="4"
                        ></circle>
                        <path
                            className="opacity-75"
                            fill="currentColor"
                            d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4z"
                        ></path>
                    </svg>
                </div>
            )}
            {hasError ? (
                <Image
                    src={placeholderSrc || ''}
                    alt="Placeholder"
                    fill  // Use fill layout
                    style={{ objectFit: 'contain' }}
                    className="rounded-full"
                    sizes="(max-width: 768px) 100vw,
                           (max-width: 1200px) 50vw,
                           33vw"
                />
            ) : (
                <Image
                    src={src}
                    alt={alt}
                    fill  // Use fill layout
                    style={{ objectFit: 'contain' }}
                    className={`${className} transition-opacity duration-500 ${isLoading ? 'opacity-0' : 'opacity-100'}`}
                    onLoad={handleLoad}
                    onError={handleError}
                    sizes="(max-width: 768px) 100vw,
                           (max-width: 1200px) 50vw,
                           33vw"
                />
            )}
        </div>
    );
};

export default ImageWithLoader;
