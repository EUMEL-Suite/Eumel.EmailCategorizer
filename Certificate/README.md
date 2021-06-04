# Create trust 

Currently, a self signed certificate and a intermediate CA needs to be added to the certificate store.

Add `eumel-ca.crt` to the "Trusted Root Certification Authorities"

Add `eumel-categorizer-ca.crt` to the "Trusted Publishers"


This will install the entire validation chain to the certificate store and trust the addin.

**BEWARE:** In case my personal root CA or intermediate CA got compromized, malicious code which is signed with the compromized certificate is trusted. This will bring your machine at a rist.