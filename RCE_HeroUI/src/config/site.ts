export type SiteConfig = typeof siteConfig;

export const siteConfig = {
  name: "Reserva con Enanos",
  description: "Reserva con Enanos.",
  navItems: [
    {
      label: "Home",
      href: "/",
    },
    {
      label: "Providers",
      href: "/providers",
    },
  ],
  navMenuItems: [
    {
      label: "Profile",
      href: "/profile",
    },
  ],
  links: {
    github: "https://github.com/frontio-ai/heroui",
  },
};
