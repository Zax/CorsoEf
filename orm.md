# Object-relational mapping (ORM)
da [WikiPedia](https://it.wikipedia.org/wiki/Object-relational_mapping)

In informatica l'Object-Relational Mapping (ORM) è una tecnica di programmazione che favorisce l'integrazione di sistemi software aderenti al paradigma della programmazione orientata agli oggetti con sistemi RDBMS.

Un prodotto ORM fornisce, mediante un'interfaccia orientata agli oggetti, tutti i servizi inerenti alla persistenza dei dati, astraendo nel contempo le caratteristiche implementative dello specifico RDBMS utilizzato.

I principali vantaggi nell'uso di un tale sistema sono i seguenti.

* Il superamento (più o meno completo) dell'incompatibilità di fondo tra il progetto orientato agli oggetti ed il modello relazionale sul quale è basata la maggior parte degli attuali RDBMS utilizzati.

* Un'elevata **portabilità** rispetto alla tecnologia DBMS utilizzata: cambiando DBMS non devono essere riscritte le routine che implementano lo strato di persistenza; generalmente basta cambiare poche righe nella configurazione del prodotto per l'ORM utilizzato.

* Drastica riduzione della quantità di codice sorgente da redigere; l'ORM maschera dietro semplici comandi le complesse attività di creazione, prelievo, aggiornamento ed eliminazione dei dati (dette CRUD - Create, Read, Update, Delete). Tali attività occupano di solito una buona percentuale del tempo di stesura, testing e manutenzione complessivo. Inoltre, sono per loro natura molto ripetitive e, dunque, favoriscono la possibilità che vengano commessi errori durante la stesura del codice che le implementa.

* Suggerisce la realizzazione dell'architettura di un sistema software mediante approccio stratificato, tendendo pertanto ad isolare in un solo livello la logica di persistenza dei dati, a vantaggio della modularità complessiva del sistema.

I prodotti per l'ORM attualmente più diffusi offrono spesso nativamente funzionalità che altrimenti andrebbero realizzate manualmente dal programmatore:

* Caricamento automatico del grafo degli oggetti secondo i legami di associazione definiti a livello di linguaggio. Il caricamento di un'ipotetica istanza della classe Studente, potrebbe automaticamente produrre il caricamento dei dati collegati sugli esami sostenuti. Tale caricamento, in più, può avvenire solo se il dato è effettivamente richiesto dal programma, ed è altrimenti evitato (tecnica nota con il nome di **lazy-initialization**).

* **Gestione della concorrenza** nell'accesso ai dati durante conversazioni. Conflitti durante la modifica di un dato da parte di più utenti in contemporanea, possono essere automaticamente rilevati dal sistema ORM.

* **Meccanismi di caching dei dati**. Per esempio, se accade che uno stesso dato venga prelevato più volte dal RDBMS, il sistema ORM può fornire automaticamente un supporto al caching che migliori le prestazioni dell'applicazione e riduca il carico sul sistema DBMS.

* Gestione di una conversazione mediante uso del design pattern **Unit of Work**, che ritarda tutte le azioni di aggiornamento dei dati al momento della chiusura della conversazione; in questo modo le richieste inviate al RDBMS sono quelle strettamente indispensabili (per es. viene eseguita solo l'ultima di una serie di update su uno stesso dato, oppure non viene eseguita affatto una serie di update su di un dato che in seguito viene eliminato); inoltre il colloquio con il DBMS avviene mediante composizione di query multiple in un unico statement, limitando così al minimo il numero di round-trip-time richiesti e, conseguentemente, i tempi di risposta dell'applicazione.

L'uso di un ORM favorisce il raggiungimento di più alti standard qualitativi software, migliorando in particolare le caratteristiche di correttezza, manutenibilità, evolvibilità e portabilità.

Ecco una [lista](https://en.wikipedia.org/wiki/List_of_object-relational_mapping_software) degli ORM conosciuti suddivisi per linguaggi.

