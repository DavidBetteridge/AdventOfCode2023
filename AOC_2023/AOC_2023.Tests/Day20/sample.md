```mermaid
  graph TD;
    broadcaster--> A;
    broadcaster--> B;
    broadcaster--> C;
    B --> B;
    B --> C;
    C --> INV;
    INV --> A;
```

