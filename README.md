# UI Fragments Catalog

## Fragments

Fragments are composable UI blocks in compostion through Server Side Inclusion (SSI) using Edge Side Includes ([ESI](https://www.w3.org/TR/esi-lang)) or Client Side Includsion (CSI) using [h-include](https://github.com/gustafnk/h-include).

The fragments project is a "catalog" of fragments, think "swagger for UI": what fragments are shared by this application, and how to inlcude them.

A fragment consists of three parts: the markup, styles and behavior (js). We use the following convension:

- fragment.css.html
- fragment.js.html
- fragment.html

To include using ESI, on inclution per instance:

```html
  <esi:include src="fragment.html"></esi:include>
```

The other two parts is included per "type" and could be placed in the document as you see fit:

```html
  <esi:include src="fragment.css.html"></esi:include>
```

```html
  <esi:include src="fragment.js.html"></esi:include>
```

Using ".html" lets the producer control versioning and cache busting.

The fragments project expect fragments to use the following url: "/fragments/fragment.html. All fragments in this path will be "scanned" and presented in the "fragment UI catalog".

## A tale of two folds

ESI limits you to 64 inclusions. But when optimizing your page, you might want to mix'n'match strategies. For example using SSI above the fold and CSI below the fold. [h-include](https://github.com/gustafnk/h-include) lets you compose lazily and optimize below the fold.

All fragments that follow the path convensions are listed in the "fragment UI catalog" – how you compose is a implementation detail.

## Baseline(s)

There might be use for a thin layer of common dependencies like css reset, fonts etc. The "fragments UI catalog" supports both a styling baseline and, if needed, a behavior baseline (js).
