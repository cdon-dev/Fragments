# UI Fragmens Catalog

## Fragments

Fragments is composable UI block in compostion trough Server Side Inclusion (SSI) using Edge Side Includes ([ESI](https://www.w3.org/TR/esi-lang)) or Client Side Includsion (CSI) using [h-include](https://github.com/gustafnk/h-include).

The fragments project is a "catlog" of fragments, think "swagger for UI", what fragments are shared by this application, and how to inlcude them.

A fragment consists of three part, the markup, styles and behavior (js), using the following convension;

- fragment.css.html
- fragment.js.html
- fragment.html

To include using ESI, on inclution per instance.

```html
  <esi:include src="fragment.html"></esi:include>
```

The other two parts is included per "type", and could be placed in the document as you see fit.

```html
  <esi:include src="fragment.css.html"></esi:include>
```

```html
  <esi:include src="fragment.js.html"></esi:include>
```

Using ".html" lets the produces control versioning and cache-busting.

The fragments project expect fragments to use the following url "/fragments/fragment.html. All fragments in this path will be "scanned" and presented in the "fragement UI catalog".

## A tale of two folds

ESI limits you to 64 inlculsions. But optmizing your page, you might want to mix n match strategies. For ex using SSI above the fold, and CSI below the fold. [h-include](https://github.com/gustafnk/h-include) let you compose lazily and optimizing below the fold strategies.

All fragments the follows the path convensions is listed in the "fragment UI catalog", how you compose is a implementation detail.

## Baseline(s)

There might be use for a thin layer of common this like css-reset, fonts etc. The "fragments UI catalog" support both a styling baseline and if needed a behavior baseline (js).