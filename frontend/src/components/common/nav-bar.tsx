'use client'

import React, { useState } from 'react';
import Link from 'next/link';
import { FC } from 'react';
import {IdentityControl} from "@/msal/identity-control";

// Define the props for the NavBar component
export interface NavBarProps {
    brandName: string;
    logoSvg: React.ReactNode;
    links?: Array<{ name: string; href: string }>; // links is now optional
}

const NavBar: FC<NavBarProps> = ({ brandName, logoSvg, links = [] }) => { // Default value for links is an empty array
    const [isOpen, setIsOpen] = useState(false);

    const toggleMenu = () => {
        setIsOpen(!isOpen);
    };

    const closeMenu = () => {
        setIsOpen(false);
    };

    return (
        <nav className="bg-white shadow-md fixed top-0 w-full z-50">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-16">
                    {/* Logo and Brand Name inside Link */}
                    <div className="flex-shrink-0">
                        <Link href="/" className="flex items-center space-x-2">
                            {/* SVG Logo */}
                            <span>{logoSvg}</span>
                            {/* Brand Name - Hidden on mobile */}
                            <span className="hidden md:block text-xl font-bold text-gray-800">
                                {brandName}
                            </span>
                        </Link>
                    </div>

                    {/* Links and IdentityControl for desktop view */}
                    <div className="hidden md:flex items-center space-x-4">
                        {links.length > 0 && (
                            <div className="ml-10 flex items-baseline space-x-4">
                                {links.map((link) => (
                                    <Link
                                        key={link.name}
                                        href={link.href}
                                        className="text-gray-800 hover:text-gray-400 px-3 py-2 rounded-md text-sm font-extrabold"
                                    >
                                        {link.name}
                                    </Link>
                                ))}
                            </div>
                        )}
                        {/* IdentityControl component on the right for desktop */}
                        <div>
                            <IdentityControl />
                        </div>
                    </div>

                    {/* Mobile Menu Button */}
                    <div className="-mr-2 flex md:hidden">
                        <button
                            onClick={toggleMenu}
                            className="text-gray-800 hover:text-gray-400 inline-flex items-center justify-center p-2 rounded-md focus:outline-none"
                        >
                            <svg
                                className="h-6 w-6"
                                fill="none"
                                stroke="currentColor"
                                viewBox="0 0 24 24"
                            >
                                {isOpen ? (
                                    <path
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                        strokeWidth="2"
                                        d="M6 18L18 6M6 6l12 12"
                                    />
                                ) : (
                                    <path
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                        strokeWidth="2"
                                        d="M4 6h16M4 12h16m-7 6h7"
                                    />
                                )}
                            </svg>
                        </button>
                    </div>
                </div>
            </div>

            {/* Mobile Menu (visible when isOpen is true) */}
            {isOpen && (
                <div className="md:hidden bg-white shadow-lg">
                    <div className="px-2 pt-2 pb-3 space-y-1">
                        {links.length > 0 && links.map((link) => (
                            <Link
                                key={link.name}
                                href={link.href}
                                className="block text-gray-800 hover:text-gray-400 px-3 py-2 rounded-md text-base font-medium"
                                onClick={closeMenu}
                            >
                                {link.name}
                            </Link>
                        ))}
                        <IdentityControl isMobile={true}/>
                    </div>
                </div>
            )}
        </nav>
    );
};

export default NavBar;
