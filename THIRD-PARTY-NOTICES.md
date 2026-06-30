# Third-Party Notices

This repository bundles source code from third-party projects. Each retains the
license under which it was originally released. Because one of these components
(iText 5) is licensed under the **AGPL-3.0**, the repository as a whole is
distributed under the AGPL-3.0 (see [`LICENSE`](LICENSE)).

| Component | Location | License | Notes |
|-----------|----------|---------|-------|
| **iText 5 (iTextSharp)** | `eSign/` | AGPL-3.0 | PDF engine. License text: `eSign/text/AGPL.txt`, `eSign/text/LICENSE.txt`, `eSign/text/NOTICE.txt`. Also available from iText Group under a separate commercial license. |
| **Bouncy Castle (C#)** | `BouncyCastle/` | MIT-style (Bouncy Castle / adapted MIT X11) | Cryptography (CMS, X.509, hashing, encoders). See https://www.bouncycastle.org/csharp/licence.html |
| **JZlib / zlib port** | `System/util/zlib/` | zlib/libpng (BSD-style) | Deflate/Inflate streams used by the PDF engine. |
| **iText support utilities** | `System/util/` | AGPL-3.0 | Collections and helpers that ship as part of iTextSharp's `System.util` namespace. |

## Original code

The original eMudhra RDSA code lives in `RDSA/` and is additionally offered by
the copyright holder under the MIT license (see [`LICENSE-MIT`](LICENSE-MIT)).
This does not change the AGPL-3.0 terms that govern the combined/distributed
work as a whole.

## Replacing the AGPL dependency

If you wish to redistribute this project under a permissive license, the iText 5
component in `eSign/` must be removed and replaced with a permissively-licensed
PDF library (or used under a commercial iText license). Until then, any
distribution of this combined work must comply with the AGPL-3.0, including the
network-use (Section 13) source-availability obligation.
