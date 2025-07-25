import React from "react";
import {
  Navbar,
  NavbarBrand,
  NavbarContent,
  NavbarItem,
  NavbarMenuToggle,
  NavbarMenu,
  NavbarMenuItem,
  Link,
  Button,
} from "@heroui/react";
import { Icon } from "@iconify-icon/react";
import { Outlet } from "react-router-dom";

export default function DefaultLayout() {
  const [isMenuOpen, setIsMenuOpen] = React.useState(false);
  const [sidebarOpen, setSidebarOpen] = React.useState(true);

  const menuItems = [
    { label: "Profile", icon: "heroicons:user" },
    { label: "Dashboard", icon: "heroicons:home" },
    { label: "Activity", icon: "heroicons:bolt" },
    { label: "Analytics", icon: "heroicons:chart-bar" },
    { label: "System", icon: "heroicons:cog" },
    { label: "Deployments", icon: "heroicons:cloud-upload" },
    { label: "My Settings", icon: "heroicons:adjustments-horizontal" },
    { label: "Team Settings", icon: "heroicons:users" },
    { label: "Help & Feedback", icon: "heroicons:question-mark-circle" },
    { label: "Log Out", icon: "heroicons:arrow-right-on-rectangle" },
  ];
  return (
    <div className="flex h-screen">
      {/* Sidebar para tablet/PC */}
      <div
        className={`hidden sm:flex flex-col bg-white border-r border-gray-100 shadow-lg transition-all duration-300 ${sidebarOpen ? "w-64" : "w-20"}`}
      >
        <div className="flex items-center justify-between px-4 py-4 border-b border-gray-100">
          <div className="flex items-center gap-2">
            {sidebarOpen && (
              <Icon icon="heroicons:squares-2x2" width="32" height="32" />
            )}
          </div>
          <Button
            isIconOnly
            variant="light"
            onPress={() => setSidebarOpen((v) => !v)}
            aria-label={sidebarOpen ? "Cerrar menú" : "Abrir menú"}
          >
            <Icon
              icon={
                sidebarOpen ? "heroicons:arrow-left" : "heroicons:arrow-right"
              }
              width="20"
              height="20"
            />
          </Button>
        </div>
        <nav className="flex-2 flex flex-col gap-2 mt-6">
          {menuItems.map((item) => (
            <Link
              key={item.label}
              href="#"
              className={`flex items-center gap-3 px-4 py-2 rounded-lg text-green-700 font-semibold hover:bg-green-50 hover:text-green-900 transition-colors ${sidebarOpen ? "" : "justify-center"}`}
            >
              <span className="inline-block">
                <Icon icon={`${item.icon}`} width="20" height="20" />
              </span>
              {sidebarOpen && <span>{item.label}</span>}
            </Link>
          ))}
        </nav>
      </div>
      {/* Navbar superior y menú móvil */}
      <div className="flex-1 flex flex-col">
        <Navbar
          onMenuOpenChange={setIsMenuOpen}
          className="border-b border-gray-100 shadow"
        >
          <NavbarContent>
            <NavbarMenuToggle
              aria-label={isMenuOpen ? "Close menu" : "Open menu"}
              className="sm:hidden"
            />
            <NavbarBrand>
              <span className="font-bold text-green-700 text-xl ml-2">RCE</span>
            </NavbarBrand>
          </NavbarContent>
          <NavbarContent justify="end">
            <NavbarItem className="flex">
              <Button as={Link} color="primary" href="#" variant="flat">
                Login
              </Button>
            </NavbarItem>
          </NavbarContent>
          <NavbarMenu>
            {menuItems.map((item, index) => (
              <NavbarMenuItem key={`${item.label}-${index}`}>
                <Link
                  className="w-full flex items-center gap-3"
                  color={
                    index === 2
                      ? "primary"
                      : index === menuItems.length - 1
                        ? "danger"
                        : "foreground"
                  }
                  href="#"
                  size="lg"
                >
                  <span className="inline-block">
                    <i className={`iconify ${item.icon} text-xl`} />
                  </span>
                  <span>{item.label}</span>
                </Link>
              </NavbarMenuItem>
            ))}
          </NavbarMenu>
        </Navbar>
        <div className="mx-auto max-w-7xl">
          <Outlet />
        </div>
        <footer className="w-full flex items-center justify-center py-3">
          <Link
            isExternal
            className="flex items-center gap-1 text-current"
            href="https://heroui.com"
            title="heroui.com homepage"
          >
            <span>Powered by</span>
            <p className="font-bold">HeroUI</p>
          </Link>
        </footer>
      </div>
    </div>
  );
}
