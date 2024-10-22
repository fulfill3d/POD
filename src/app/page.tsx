"use client";

import React from "react";

export default function Home() {
  return (
      <div className="min-h-screen w-full flex flex-col items-center justify-between px-6 py-10 bg-gray-50 overflow-hidden">
        <div className="w-full h-full max-w-5xl text-center space-y-10 flex flex-col items-center justify-center">
          {/* Heading */}
          <h1 className="text-5xl md:text-7xl font-extrabold text-gray-900 leading-tight">
            POD Demo Coming Soon
          </h1>

          {/* Short Description */}
          <p className="text-lg md:text-2xl text-gray-700 leading-relaxed">
            The demo for the Print on Demand (POD) solution is not ready yet.
            <br />
            <br />
            You can explore the source code or check out the documentation to learn more about the app.
          </p>

          {/* Call to Action (CTA) Cards */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6 w-full max-w-3xl">
            {/* GitHub Source Code Card */}
            <div
                onClick={() => window.open('https://github.com/fulfill3d/POD_Frontend', '_blank')}
                className="cursor-pointer p-6 bg-indigo-600 text-white rounded-lg shadow-lg transition-transform transform hover:scale-105 hover:shadow-xl"
            >
              <h2 className="text-2xl font-bold">View Source Code</h2>
              <p className="mt-2 text-lg">Check out the GitHub repository for the frontend code.</p>
            </div>

            {/* Documentation Card */}
            <div
                onClick={() => window.open('https://fulfill3d.com/projects/4f7334bd-f946-446e-81e7-77ea52c5fb9c', '_blank')}
                className="cursor-pointer p-6 bg-teal-600 text-white rounded-lg shadow-lg transition-transform transform hover:scale-105 hover:shadow-xl"
            >
              <h2 className="text-2xl font-bold">Read Documentation</h2>
              <p className="mt-2 text-lg">Learn more about how the app works from the official docs.</p>
            </div>
          </div>
        </div>

        {/* Link to Fulfill3D Frontend Repo */}
        <footer className="text-gray-600 font-semibold text-sm mt-10">
          <a
              href="https://github.com/fulfill3d/POD_Frontend"
              target="_blank"
              rel="noopener noreferrer"
              className="hover:underline"
          >
            View src of this app
          </a>
        </footer>
      </div>
  );
}
