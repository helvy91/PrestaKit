import type {ReactNode} from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import CodeBlock from '@theme/CodeBlock';
import Heading from '@theme/Heading';
import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

import HomepageFeatures from '@site/src/components/HomepageFeatures';

import styles from './index.module.css';

function HomepageHeader() {
  const {siteConfig} = useDocusaurusContext();
  return (
    <header className={clsx('hero', styles.heroBanner)}>
      <div className="container">
        <img
          src="img/logo.png"
          alt="PrestaKit"
          className={styles.heroLogo}
        />
        <Heading as="h1" className={styles.heroTitle}>
          {siteConfig.title}
        </Heading>
        <p className={styles.heroSubtitle}>{siteConfig.tagline}</p>
        <div className={styles.buttons}>
          <Link
            className="button button--primary button--lg"
            to="/getting-started/installation">
            Get Started
          </Link>
          <Link
            className="button button--secondary button--lg"
            href="https://www.nuget.org/packages/PrestaKit">
            View on NuGet
          </Link>
        </div>
      </div>
    </header>
  );
}

function ProblemSolution() {
  return (
    <section className={styles.problem}>
      <div className="container">
        <div className={styles.problemInner}>
          <Heading as="h2">A simple PrestaShop webservice client</Heading>
          <p>
            PrestaKit lets you automate your shop in C#. Upload products with images, sync catalogs across shops, translate 
            content with LLMs, build custom UIs on a clean API, generate reports, whatever your business needs. 
            All from a small, modern NuGet package with zero dependencies.
          </p>
        </div>
      </div>
    </section>
  );
}

function CodeComparison() {
  return (
    <section className={styles.comparison}>
      <div className="container">
        <Heading as="h2" className={styles.comparisonHeading}>
          Write this, not that
        </Heading>
        <div className={styles.comparisonGrid}>
          <div className={styles.codeCol}>
            <span className={clsx(styles.codeLabel, styles.codeLabelBad)}>
              Without PrestaKit
            </span>
            <img style={{ borderRadius: '15px' }} src="img/before-after/before.webp" />
          </div>
          <div className={styles.codeCol}>
            <span className={clsx(styles.codeLabel, styles.codeLabelGood)}>
              With PrestaKit
            </span>
            <img style={{ borderRadius: '15px' }} src="img/before-after/after.webp" />
          </div>
        </div>
      </div>
    </section>
  );
}

const installPrestaKit = `dotnet add package PrestaKit`;

const installDI = `dotnet add package PrestaKit.Extensions.DependencyInjection`;

const usageManual = `var http = new HttpClient { BaseAddress = new Uri("https://your-shop.com/api/") };
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("YOUR_API_KEY:")));

var client = new PrestaShopClient(http);

var product = await client.Products.GetAsync(1);`;

const usageDI = `// Program.cs
builder.Services.AddPrestaShopClient(options =>
{
    options.BaseUrl = new Uri("https://your-shop.com/api/");
    options.ApiKey  = "YOUR_API_KEY";
});

// anywhere via constructor injection
public class ProductService(IPrestaShopClient client)
{
    public Task<Product> GetAsync(long id) => client.Products.GetAsync(id);
}`;

function QuickStart() {
  return (
    <section className={styles.quickstart}>
      <div className="container">
        <Heading as="h2" className={styles.quickstartHeading}>
          Up and running in minutes
        </Heading>
        <Tabs groupId="setup">
          <TabItem value="manual" label="Manual">
            <CodeBlock language="bash">{installPrestaKit}</CodeBlock>
            <CodeBlock language="csharp">{usageManual}</CodeBlock>
          </TabItem>
          <TabItem value="di" label="Dependency Injection">
            <CodeBlock language="bash">{installPrestaKit}</CodeBlock>
            <CodeBlock language="bash">{installDI}</CodeBlock>
            <CodeBlock language="csharp">{usageDI}</CodeBlock>
          </TabItem>
        </Tabs>
        <div className={styles.quickstartButtons}>
          <Link className="button button--primary button--lg" to="/intro">
            Read the docs
          </Link>
        </div>
      </div>
    </section>
  );
}

export default function Home(): ReactNode {
  const {siteConfig} = useDocusaurusContext();
  return (
    <Layout
      title={siteConfig.title}
      description="A modern, typed .NET client for the PrestaShop webservice API.">
      <HomepageHeader />
      <main>
        <ProblemSolution />
        <HomepageFeatures />
        <CodeComparison />
        <QuickStart />
      </main>
    </Layout>
  );
}
