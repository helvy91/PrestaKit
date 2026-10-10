import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

// This runs in Node.js - Don't use client-side code here (browser APIs, JSX...)

const config: Config = {
  title: 'PrestaKit',
  tagline: 'PrestaKit | C# .NET Client for the PrestaShop webservice API',
  favicon: 'img/favicon.ico',

  future: {
    v4: true,
  },

  url: 'https://helvy91.github.io',
  baseUrl: '/',
  organizationName: 'helvy91',
  projectName: 'PrestaKit',

  onBrokenLinks: 'warn',

  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          sidebarPath: './sidebars.ts',
          routeBasePath: '/',                     // serve docs at the site root (no /docs prefix)
          editUrl: 'https://github.com/helvy91/PrestaKit/tree/main/website/docs/',
        },
        blog: false,
        theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    image: 'img/social-card.png',
    colorMode: {
      respectPrefersColorScheme: true,
    },
    metadata: [
      { name: 'keywords', content: 'prestashop, .net, c#, api, client, webservice, nuget' },
      { name: 'description', content: 'A modern, typed .NET client for the PrestaShop webservice API. Typed entities, fluent queries, exception handling.' },
    ],
    navbar: {
      title: 'PrestaKit',
      logo: {
        alt: 'PrestaKit',
        src: 'img/logo.png',
      },
      items: [
        {
          type: 'dropdown',
          label: 'Getting Started',
          position: 'left',
          items: [
            { label: 'Installation', to: '/getting-started/installation' },
            { label: 'Setup', to: '/getting-started/setup' },
            { label: 'First Request', to: '/getting-started/first-request' },
          ],
        },
        // PrestaShopClient — single page → plain link
        {
          label: 'PrestaShopClient',
          to: '/client',
          position: 'left',
        },
        // ResourceClient<T> — has subpages → dropdown
        {
          type: 'dropdown',
          label: 'ResourceClient<T>',
          position: 'left',
          items: [
            { label: 'Reading', to: '/resources/reading' },
            { label: 'Listing', to: '/resources/listing' },
            { label: 'Creating & Updating', to: '/resources/creating-updating' },
            { label: 'EnumerateAsync', to: '/resources/enumerate' },
          ],
        },
        // Query<T> — has subpages → dropdown
        {
          label: 'Query<T>',
          to: '/query-builder',
          position: 'left'
        },
        // Exceptions — single page → plain link
        {
          label: 'Exceptions',
          to: '/error-handling',
          position: 'left',
        },
        {
          href: 'https://www.nuget.org/packages/PrestaKit',
          label: 'NuGet',
          position: 'right',
        },
        {
          href: 'https://github.com/helvy91/PrestaKit',
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            { label: 'Getting Started', to: '/getting-started/installation' },
            { label: 'PrestaShopClient', to: '/client' },
            { label: 'ResourceClient<T>', to: '/resources/reading' },
            { label: 'Query<T>', to: '/query-builder' },
          ],
        },
        {
          title: 'Package',
          items: [
            {
              label: 'NuGet',
              href: 'https://www.nuget.org/packages/PrestaKit',
            },
          ],
        },
        {
          title: 'More',
          items: [
            {
              label: 'GitHub',
              href: 'https://github.com/helvy91/PrestaKit',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Wiktor Helak. Built with Docusaurus.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
      additionalLanguages: ['csharp'],           // enable C# syntax highlighting
    },
  } satisfies Preset.ThemeConfig,
};

export default config;