(function () {
    "use strict";

    // ---------- Video sesi aç/kapa (hero + biz sayfası ortak) ----------
    document.querySelectorAll(".hero-video__frame, .biz-video__frame").forEach((frame) => {
        const isBiz = frame.classList.contains("biz-video__frame");
        const playerSel = isBiz ? ".biz-video__player" : ".hero-video__player";
        const soundSel = isBiz ? ".biz-video__sound" : ".hero-video__sound";
        const player = frame.querySelector(playerSel);
        const soundBtn = frame.querySelector(soundSel);
        if (!player || !soundBtn) return;
        const setState = (muted) => {
            player.muted = muted;
            soundBtn.dataset.state = muted ? "muted" : "unmuted";
            soundBtn.setAttribute("aria-label", muted ? "Sesi aç" : "Sesi kapat");
        };
        soundBtn.addEventListener("click", () => {
            const willUnmute = player.muted;
            setState(!willUnmute);
            if (willUnmute) {
                player.play().catch(() => { /* yoksay */ });
            }
        });
        document.addEventListener("visibilitychange", () => {
            if (document.hidden) player.pause();
            else player.play().catch(() => { /* yoksay */ });
        });
    });

    // ---------- Tema (dark / light) toggle ----------
    const themeToggle = document.querySelector(".theme-toggle");
    if (themeToggle) {
        themeToggle.addEventListener("click", () => {
            const current = document.documentElement.getAttribute("data-theme") || "dark";
            const next = current === "dark" ? "light" : "dark";
            document.documentElement.setAttribute("data-theme", next);
            try { localStorage.setItem("creida-theme", next); } catch (e) { /* yoksay */ }
        });
    }

    // ---------- Sticky header tonu ----------
    const header = document.getElementById("site-header");
    const onScroll = () => {
        if (!header) return;
        if (window.scrollY > 24) header.classList.add("is-scrolled");
        else header.classList.remove("is-scrolled");
    };
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });

    // ---------- Mobil menü (tam sitemap paneli) ----------
    const toggle = document.querySelector(".menu-toggle");
    const mobileNav = document.getElementById("mobile-nav");
    if (toggle && mobileNav) {
        toggle.addEventListener("click", () => {
            const isOpen = !mobileNav.hidden;
            if (isOpen) {
                mobileNav.hidden = true;
                toggle.setAttribute("aria-expanded", "false");
                toggle.setAttribute("aria-label", "Menüyü aç");
                document.body.style.overflow = "";
            } else {
                mobileNav.hidden = false;
                toggle.setAttribute("aria-expanded", "true");
                toggle.setAttribute("aria-label", "Menüyü kapat");
                document.body.style.overflow = "hidden";
            }
        });
        mobileNav.querySelectorAll("a").forEach(a => {
            a.addEventListener("click", () => {
                mobileNav.hidden = true;
                toggle.setAttribute("aria-expanded", "false");
                document.body.style.overflow = "";
            });
        });
    }

    // ---------- ⌘K hint butonu — kbar'ı aç ----------
    document.querySelectorAll("[data-open-kbar]").forEach(btn => {
        btn.addEventListener("click", () => {
            const kbar = document.getElementById("kbar");
            if (kbar) {
                kbar.hidden = false;
                const input = document.getElementById("kbar-input");
                if (input) { input.value = ""; setTimeout(() => input.focus(), 30); }
                document.body.style.overflow = "hidden";
            }
        });
    });

    // ---------- Scroll reveal ----------
    const supportsIO = "IntersectionObserver" in window;
    const reveals = document.querySelectorAll(".reveal, .reveal-stagger");

    // İlk yüklemede viewport'ta görünen her şeyi anında is-visible yap
    // (IntersectionObserver bazen ilk render'da intersect event tetiklemiyor)
    reveals.forEach(el => {
        const rect = el.getBoundingClientRect();
        if (rect.top < window.innerHeight && rect.bottom > 0) {
            el.classList.add("is-visible");
        }
    });

    if (supportsIO && reveals.length) {
        const io = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("is-visible");
                    io.unobserve(entry.target);
                }
            });
        }, { threshold: 0.12, rootMargin: "0px 0px -8% 0px" });

        reveals.forEach(el => io.observe(el));
    } else {
        reveals.forEach(el => el.classList.add("is-visible"));
    }

    // ---------- Smooth anchor scroll (offset) ----------
    const prefersReducedMotion = window.matchMedia &&
        window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    document.querySelectorAll('a[href^="#"]').forEach(link => {
        link.addEventListener("click", (e) => {
            const id = link.getAttribute("href");
            if (!id || id === "#") return;
            const target = document.querySelector(id);
            if (!target) return;
            e.preventDefault();
            const headerOffset = (header ? header.offsetHeight : 0) + 12;
            const y = target.getBoundingClientRect().top + window.scrollY - headerOffset;
            window.scrollTo({ top: y, behavior: prefersReducedMotion ? "auto" : "smooth" });
        });
    });

    // ---------- Cmd+K komut paleti ----------
    const kbar = document.getElementById("kbar");
    const kbarInput = document.getElementById("kbar-input");
    const kbarList = document.getElementById("kbar-list");

    const commands = [
        { label: "Ana sayfa",          hint: "Hero, hizmetler, işler",        href: "/",          group: "Sayfalar" },
        { label: "İşler",              hint: "Tüm case study'ler",            href: "/#work",     group: "Sayfalar" },
        { label: "Hizmetler",          hint: "6 disiplin",                    href: "/#services", group: "Sayfalar" },
        { label: "Hakkımızda",         hint: "Manifesto, prensipler, ödüller", href: "/#about",    group: "Sayfalar" },
        { label: "Süreç",              hint: "5 fazlık çalışma akışı",        href: "/process",   group: "Sayfalar" },
        { label: "Ekip",               hint: "Kurucular ve özgeçmişler",      href: "/team",      group: "Sayfalar" },
        { label: "Insights",           hint: "Yazılar ve notlar",             href: "/insights",  group: "Sayfalar" },
        { label: "Yapmadıklarımız",    hint: "Manifesto / 8 kural",           href: "/manifesto", group: "Sayfalar" },
        { label: "Fiyatlandırma",      hint: "Şeffaf bütçe aralıkları",       href: "/pricing",   group: "Sayfalar" },
        { label: "Sonuçlar",           hint: "Canlı kampanya metrikleri",     href: "/results",   group: "Sayfalar" },
        { label: "Müsaitlik",          hint: "Önümüzdeki altı ay",            href: "/availability", group: "Sayfalar" },
        { label: "Brief Sihirbazı",    hint: "8 soruda brief oluştur",        href: "/brief",     group: "Sayfalar" },
        { label: "Yıllık rapor 2025",  hint: "Açık defter",                   href: "/report",    group: "Sayfalar" },
        { label: "Basın",              hint: "Logo, marka kitabı, foto",      href: "/press",     group: "Sayfalar" },
        { label: "Kaynaklar",          hint: "Ücretsiz şablonlar",            href: "/resources", group: "Sayfalar" },
        { label: "İletişim",           hint: "Brief gönder",                  href: "/#contact",  group: "Sayfalar" },

        { label: "Tüm sektörler",      hint: "Sektör index",                  href: "/sectors",          group: "Sektörler" },
        { label: "Fintech için",       hint: "Sektör sayfası",                href: "/sectors/fintech",   group: "Sektörler" },
        { label: "DTC / E-ticaret için", hint: "Sektör sayfası",              href: "/sectors/dtc",       group: "Sektörler" },
        { label: "B2B SaaS için",      hint: "Sektör sayfası",                href: "/sectors/b2b-saas",  group: "Sektörler" },
        { label: "Kariyer",            hint: "Açık pozisyonlar",              href: "/careers",          group: "Sayfalar" },

        { label: "Ayla Botanik",       hint: "Marka kimliği · Dijital lansman", href: "/work/ayla-botanik",      group: "İşler" },
        { label: "NorthGate",          hint: "Konumlandırma · LinkedIn Ads",    href: "/work/northgate",         group: "İşler" },
        { label: "Lume Coffee",        hint: "Ambalaj · Mağaza deneyimi",       href: "/work/lume-coffee",       group: "İşler" },
        { label: "Veris Finans",       hint: "Marka tazeleme · Kullanıcı edinimi", href: "/work/veris-finans",   group: "İşler" },
        { label: "Maden Su Premium",   hint: "Konumlandırma · TVC",             href: "/work/maden-su-premium",  group: "İşler" },
        { label: "Voltra Electric",    hint: "360° lansman",                    href: "/work/voltra-electric",   group: "İşler" },

        { label: "Karanlık moda geç",  hint: "Tema: dark",                    action: () => setTheme("dark"),    group: "Eylemler" },
        { label: "Aydınlık moda geç",  hint: "Tema: light",                   action: () => setTheme("light"),   group: "Eylemler" },
        { label: "Türkçe",             hint: "TR — site dili",                href: "/culture/set?c=tr&returnUrl=/", group: "Dil" },
        { label: "English",            hint: "EN — site language",            href: "/culture/set?c=en&returnUrl=/", group: "Dil" },
        { label: "Brief gönder",       hint: "İletişim formuna git",          href: "/#contact",                 group: "Eylemler" },
        { label: "E-posta",            hint: "hello@creida.co",               href: "mailto:hello@creida.co",    group: "Eylemler" }
    ];

    let kbarOpen = false;
    let kbarFiltered = commands.slice();
    let kbarActiveIndex = 0;

    function setTheme(theme) {
        document.documentElement.setAttribute("data-theme", theme);
        try { localStorage.setItem("creida-theme", theme); } catch (e) { /* yoksay */ }
    }

    function openKbar() {
        if (!kbar) return;
        kbar.hidden = false;
        kbarOpen = true;
        kbarInput.value = "";
        kbarFiltered = commands.slice();
        kbarActiveIndex = 0;
        renderKbar();
        setTimeout(() => kbarInput.focus(), 30);
        document.body.style.overflow = "hidden";
    }

    function closeKbar() {
        if (!kbar) return;
        kbar.hidden = true;
        kbarOpen = false;
        document.body.style.overflow = "";
    }

    function renderKbar() {
        if (!kbarList) return;
        if (kbarFiltered.length === 0) {
            kbarList.innerHTML = '<li class="kbar-empty">Eşleşme yok.</li>';
            return;
        }

        const groups = {};
        kbarFiltered.forEach((c, idx) => {
            if (!groups[c.group]) groups[c.group] = [];
            groups[c.group].push({ cmd: c, idx });
        });

        let html = "";
        Object.keys(groups).forEach(g => {
            html += `<li class="kbar-group-label">${g}</li>`;
            groups[g].forEach(({ cmd, idx }) => {
                const active = idx === kbarActiveIndex ? " is-active" : "";
                html += `
                    <li class="kbar-item${active}" data-idx="${idx}" role="option">
                        <span class="kbar-item-label">${cmd.label}</span>
                        <span class="kbar-item-hint">${cmd.hint}</span>
                    </li>`;
            });
        });
        kbarList.innerHTML = html;

        kbarList.querySelectorAll(".kbar-item").forEach(el => {
            el.addEventListener("mouseenter", () => {
                kbarActiveIndex = parseInt(el.getAttribute("data-idx"), 10);
                kbarList.querySelectorAll(".kbar-item").forEach(x => x.classList.remove("is-active"));
                el.classList.add("is-active");
            });
            el.addEventListener("click", () => {
                kbarActiveIndex = parseInt(el.getAttribute("data-idx"), 10);
                executeKbar();
            });
        });
    }

    function filterKbar(q) {
        const term = q.toLowerCase().trim();
        kbarFiltered = !term
            ? commands.slice()
            : commands.filter(c =>
                c.label.toLowerCase().includes(term) ||
                (c.hint && c.hint.toLowerCase().includes(term)));
        kbarActiveIndex = 0;
        renderKbar();
    }

    function executeKbar() {
        const cmd = kbarFiltered[kbarActiveIndex];
        if (!cmd) return;
        closeKbar();
        if (cmd.action) { cmd.action(); return; }
        if (cmd.href) { window.location.href = cmd.href; }
    }

    if (kbar && kbarInput) {
        kbarInput.addEventListener("input", e => filterKbar(e.target.value));
        kbar.querySelectorAll("[data-close]").forEach(el => el.addEventListener("click", closeKbar));

        document.addEventListener("keydown", e => {
            const isMac = navigator.platform.toUpperCase().indexOf("MAC") >= 0;
            const cmd = isMac ? e.metaKey : e.ctrlKey;
            if (cmd && e.key.toLowerCase() === "k") {
                e.preventDefault();
                kbarOpen ? closeKbar() : openKbar();
                return;
            }
            if (!kbarOpen) return;
            if (e.key === "Escape") { e.preventDefault(); closeKbar(); }
            else if (e.key === "ArrowDown") {
                e.preventDefault();
                kbarActiveIndex = Math.min(kbarFiltered.length - 1, kbarActiveIndex + 1);
                renderKbar();
            }
            else if (e.key === "ArrowUp") {
                e.preventDefault();
                kbarActiveIndex = Math.max(0, kbarActiveIndex - 1);
                renderKbar();
            }
            else if (e.key === "Enter") { e.preventDefault(); executeKbar(); }
        });
    }

    // ---------- Toast helper ----------
    function showToast(message, duration = 3000) {
        const toast = document.getElementById("toast");
        if (!toast) return;
        toast.textContent = message;
        toast.hidden = false;
        toast.classList.add("is-visible");
        setTimeout(() => {
            toast.classList.remove("is-visible");
            setTimeout(() => { toast.hidden = true; }, 400);
        }, duration);
    }

    // ---------- Newsletter signup (footer) ----------
    const news = document.getElementById("footer-news");
    if (news) {
        news.addEventListener("submit", e => {
            e.preventDefault();
            const inp = news.querySelector("#footer-news-email");
            const email = (inp.value || "").trim();
            if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
                inp.classList.add("has-error");
                showToast("Geçerli bir e-posta yaz.");
                return;
            }
            // Server-side handler yok; mailto ile Creida'ya gönder + kullanıcıyı bilgilendir
            const subject = encodeURIComponent("Aylık Insights bültenine kayıt");
            const body = encodeURIComponent(
                "Merhaba Creida,\n\nAylık insights bültenine kaydolmak istiyorum.\nE-posta: " + email + "\n\nTeşekkürler."
            );
            window.location.href = `mailto:hello@creida.co?subject=${subject}&body=${body}`;
            showToast("E-posta uygulaman açılıyor. Mesajı göndererek kaydını tamamla.");
            inp.value = "";
        });
    }

    // ---------- Brief Sihirbazı ----------
    const briefForm = document.getElementById("brief-form");
    const briefOutput = document.getElementById("brief-output");
    const briefProgress = document.getElementById("brief-progress");
    if (briefForm && briefOutput) {
        const labels = {
            marka:       "MARKA",
            mesele:      "MESELE",
            kitle:       "HEDEF KİTLE",
            rakip:       "REKABET",
            basari:      "BAŞARI KRİTERİ",
            kacin:       "KAÇINILACAKLAR",
            butce:       "BÜTÇE",
            zaman:       "ZAMAN ÇİZELGESİ",
            senderName:  "GÖNDEREN",
            senderEmail: "İLETİŞİM"
        };

        function buildBrief() {
            const data = {};
            briefForm.querySelectorAll("[data-key]").forEach(el => {
                data[el.getAttribute("data-key")] = (el.value || "").trim();
            });

            const lines = [];
            lines.push("BRIEF — " + (data.marka ? data.marka.split("\n")[0] : "isimsiz"));
            lines.push("Hazırlayan: " + (data.senderName || "—"));
            lines.push("Tarih: " + new Date().toLocaleDateString("tr-TR"));
            lines.push("");
            lines.push("=".repeat(64));
            lines.push("");

            Object.keys(labels).forEach(k => {
                if (k === "senderName") return; // başlığa ekledik
                const val = data[k];
                if (!val) return;
                lines.push(labels[k]);
                lines.push("-".repeat(labels[k].length));
                lines.push(val);
                lines.push("");
            });

            lines.push("=".repeat(64));
            lines.push("Bu brief Creida brief sihirbazıyla hazırlandı. creida.co/brief");
            return lines.join("\n");
        }

        function refresh() {
            briefOutput.textContent = buildBrief();

            // İlerleme yüzdesi — 8 ana soruda kaçı dolduruldu
            const mainKeys = ["marka", "mesele", "kitle", "rakip", "basari", "kacin", "butce", "zaman"];
            const filled = mainKeys.filter(k => {
                const el = briefForm.querySelector(`[data-key="${k}"]`);
                return el && (el.value || "").trim().length >= 8;
            }).length;
            const pct = Math.round((filled / mainKeys.length) * 100);
            if (briefProgress) {
                briefProgress.style.setProperty("--p", pct + "%");
                const label = briefProgress.querySelector(".brief-progress-label");
                if (label) label.textContent = `${filled}/${mainKeys.length} soru · %${pct}`;
            }

            // Boş/çok kısa cevap görsel uyarısı
            mainKeys.forEach(k => {
                const el = briefForm.querySelector(`[data-key="${k}"]`);
                if (!el) return;
                const val = (el.value || "").trim();
                el.classList.toggle("has-error", val.length > 0 && val.length < 8);
            });
        }
        briefForm.addEventListener("input", refresh);
        refresh();

        function download(filename, content, mime) {
            const blob = new Blob([content], { type: mime });
            const url = URL.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.download = filename;
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            setTimeout(() => URL.revokeObjectURL(url), 500);
        }

        document.getElementById("brief-download")?.addEventListener("click", () => {
            download("creida-brief.txt", buildBrief(), "text/plain;charset=utf-8");
            showToast("Brief indirildi.");
        });

        document.getElementById("brief-email")?.addEventListener("click", () => {
            const email = briefForm.querySelector('[data-key="senderEmail"]').value.trim();
            if (!email) { showToast("Önce e-postanı yaz."); return; }
            const subject = encodeURIComponent("Creida Brief — kendi kopyam");
            const body = encodeURIComponent(buildBrief());
            window.location.href = `mailto:${email}?subject=${subject}&body=${body}`;
        });

        document.getElementById("brief-creida")?.addEventListener("click", () => {
            const subject = encodeURIComponent("Creida'ya brief");
            const body = encodeURIComponent(buildBrief());
            window.location.href = `mailto:hello@creida.co?subject=${subject}&body=${body}`;
        });
    }

    // ---------- Easter egg: Konami code ----------
    const konami = ["ArrowUp", "ArrowUp", "ArrowDown", "ArrowDown",
                    "ArrowLeft", "ArrowRight", "ArrowLeft", "ArrowRight", "b", "a"];
    let konamiIdx = 0;
    document.addEventListener("keydown", e => {
        const key = e.key;
        const expected = konami[konamiIdx];
        const match = expected.length === 1
            ? key.toLowerCase() === expected
            : key === expected;
        if (match) {
            konamiIdx++;
            if (konamiIdx === konami.length) {
                konamiIdx = 0;
                document.body.classList.add("party");
                showToast("🌳 Bir ağaç eken kazanır. (Tema: orman)", 4000);
                setTimeout(() => document.body.classList.remove("party"), 7000);
            }
        } else {
            konamiIdx = 0;
        }
    });

    // ---------- 3 adımlı iletişim formu ----------
    const stepForm = document.querySelector(".contact-form--steps");
    if (stepForm) {
        const steps = stepForm.querySelectorAll(".form-step");
        const dots = stepForm.querySelectorAll(".step-dot");
        let currentStep = 1;

        function showStep(n) {
            steps.forEach(s => {
                const i = parseInt(s.getAttribute("data-step"), 10);
                s.classList.toggle("is-current", i === n);
            });
            dots.forEach(d => {
                const i = parseInt(d.getAttribute("data-step"), 10);
                d.classList.toggle("is-active", i <= n);
            });
            currentStep = n;
            stepForm.scrollIntoView({ behavior: prefersReducedMotion ? "auto" : "smooth", block: "start" });
        }

        function validateCurrentStep() {
            const step = stepForm.querySelector(`.form-step[data-step="${currentStep}"]`);
            if (!step) return true;
            let ok = true;
            step.querySelectorAll("[data-required]").forEach(inp => {
                if (!inp.value || !inp.value.trim()) {
                    inp.classList.add("has-error");
                    ok = false;
                } else {
                    inp.classList.remove("has-error");
                }
                // basit e-posta kontrolü
                if (inp.type === "email" && inp.value && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(inp.value)) {
                    inp.classList.add("has-error");
                    ok = false;
                }
            });
            return ok;
        }

        stepForm.addEventListener("click", e => {
            if (e.target.closest("[data-next]")) {
                if (validateCurrentStep() && currentStep < steps.length) showStep(currentStep + 1);
            } else if (e.target.closest("[data-prev]")) {
                if (currentStep > 1) showStep(currentStep - 1);
            }
        });
    }

    // ---------- Case filtering (Index "İşler" bölümü) ----------
    const filterButtons = document.querySelectorAll(".filter-pill");
    const workCards = document.querySelectorAll(".work-grid > a");
    const filterState = { industry: "", year: "" };

    filterButtons.forEach(btn => {
        btn.addEventListener("click", () => {
            const type = btn.getAttribute("data-filter-type");
            const value = btn.getAttribute("data-filter-value");
            filterState[type] = value;

            // O kategoride 'is-active' güncelle
            document.querySelectorAll(`.filter-pill[data-filter-type="${type}"]`)
                .forEach(b => b.classList.remove("is-active"));
            btn.classList.add("is-active");

            // Kartları göster/gizle
            workCards.forEach(card => {
                const industry = card.getAttribute("data-industry") || "";
                const year = card.getAttribute("data-year") || "";
                const ok =
                    (!filterState.industry || industry === filterState.industry) &&
                    (!filterState.year || year === filterState.year);
                card.style.display = ok ? "" : "none";
            });
        });
    });

    // ---------- Blur-to-sharp image loading ----------
    document.querySelectorAll("img").forEach(img => {
        if (img.complete && img.naturalWidth > 0) {
            img.classList.add("is-loaded");
        } else {
            img.addEventListener("load", () => img.classList.add("is-loaded"), { once: true });
            img.addEventListener("error", () => img.classList.add("is-loaded"), { once: true });
        }
    });

})();
