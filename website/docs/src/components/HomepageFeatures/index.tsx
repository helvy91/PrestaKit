import type {ReactNode} from 'react';
import clsx from 'clsx';
import Heading from '@theme/Heading';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  image: string;
  description: ReactNode;
};

const FeatureList: FeatureItem[] = [
  {
    title: 'Entities with all available properties',
    image: 'img/features/entities-properties.webp',
    description: (
      <>
        No need to create <strong>dozens of entities</strong> with multitude of properties or manipulate XMLs manually.
        PrestaKit gives you <strong>every entity availible</strong> in PrestaShop webservice with proper serialization.
      </>
    ),
  },
  {
    title: 'Handles the Quirks',
    image: 'img/features/quirk-handling.webp',
    description: (
      <>
        Empty fields, zero-dates, boolean <code>0/1</code>, read-only fields, the
        product <code>state</code> trap. PrestaKit <strong>smooths over the webservice's
        rough edges</strong> so you don't rediscover them the hard way.
      </>
    ),
  },
  {
    title: 'Fluent, Safe Queries',
    image: 'img/features/fluent-query.webp',
    description: (
      <>
        <strong>Filter, sort, and paginate with expression-based queries.</strong>{' '}
        <code>Where(p =&gt; p.Active)</code> - compile-checked, no magic strings,
        and streaming pagination for large catalogs.
      </>
    ),
  },
  {
    title: 'Error Handling',
    image: 'img/features/exception-handling.webp',
    description: (
      <>
        PrestaShop API errors become <strong>typed exceptions</strong>. Catch PrestaShopNotFoundException, 
        PrestaShopAuthException, or PrestaShopApiException by type, each with the parsed <strong>error details to log or act on</strong>.
      </>
    ),
  },
  {
    title: 'Zero Dependencies',
    image: 'img/features/zero-dependencies.webp',
    description: (
      <>
        The core library has <strong>no runtime dependencies</strong>. Drop it into any project
        without dragging in a dependency graph. DI support is a separate,
        opt-in package.
      </>
    ),
  },
  {
    title: 'Modern .NET',
    image: 'img/features/modern-net.webp',
    description: (
      <>
        Targets <strong>.NET 8 and .NET 10</strong>. Async throughout, dependency-injection ready,
        and built for how you write C# today.
      </>
    ),
  },
];

function Feature({title, image, description}: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className={styles.feature}>
        <img src={image} alt={title} className={styles.featureImage} />
        <Heading as="h3" className={styles.featureTitle}>
          {title}
        </Heading>
        <p className={styles.featureDescription}>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures(): ReactNode {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
