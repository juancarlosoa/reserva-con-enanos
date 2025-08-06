// Layout.tsx
import React, { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import {
  Button,
  Avatar,
  Dropdown,
  DropdownTrigger,
  DropdownMenu,
  DropdownItem,
  Tooltip,
} from "@heroui/react";
import { Icon } from "@iconify-icon/react";

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  const [isSidebarCollapsed, setIsSidebarCollapsed] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();

  const getCurrentPage = () => {
    const path = location.pathname.split("/")[1] || "";
    return path;
  };

  const activeMenuItem = getCurrentPage();

  const user = {
    name: "Juan Pérez",
    email: "juan@ejemplo.com",
    avatar: "https://i.pravatar.cc/40?u=juan@ejemplo.com",
  };

  const menuItems = [
    {
      id: "",
      label: "Dashboard",
      icon: "heroicons:home",
      path: "/",
    },
    {
      id: "providers",
      label: "Proveedores",
      icon: "heroicons:building-office",
      path: "/providers",
    },
    {
      id: "settings",
      label: "Configuración",
      icon: "heroicons:cog-6-tooth",
      path: "/settings",
    },
  ];

  const handleLogin = () => setIsLoggedIn(true);
  const handleLogout = () => setIsLoggedIn(false);
  const handleMenuClick = (path: string) => navigate(path);

  const toggleSidebar = () => {
    setIsSidebarCollapsed(!isSidebarCollapsed);
  };

  return (
    <div className="flex h-screen bg-gray-50">
      <div
        className={`bg-white shadow-lg transition-all duration-300 ${
          isSidebarCollapsed ? "w-16" : "w-64"
        }`}
      >
        <div className="p-4 border-b border-gray-200 flex justify-center items-center">
          {!isSidebarCollapsed ? (
            <h2 className="text-xl font-bold text-green-700">RCE</h2>
          ) : (
            <Icon
              icon="heroicons:squares-2x2"
              className="w-7 h-7 text-green-600"
            />
          )}
        </div>
        <div className="p-2 border-b border-gray-200">
          <Button
            isIconOnly
            variant="light"
            onPress={toggleSidebar}
            className="w-full"
          >
            <Icon
              icon={
                isSidebarCollapsed
                  ? "heroicons:chevron-right"
                  : "heroicons:chevron-left"
              }
              className="w-5 h-5 text-green-600"
            />
          </Button>
        </div>
        <nav className="mt-4">
          {menuItems.map((item) => {
            const isActive = activeMenuItem === item.id;
            const MenuButton = (
              <Button
                key={item.id}
                variant="light"
                className={`w-full justify-start mb-1 rounded-md ${
                  isSidebarCollapsed ? "px-0" : "px-4"
                } ${
                  isActive
                    ? "bg-green-100 text-green-700"
                    : "hover:bg-green-50 text-gray-700"
                }`}
                onPress={() => handleMenuClick(item.path)}
              >
                <Icon icon={item.icon} className="w-5 h-5" />
                {!isSidebarCollapsed && (
                  <span className="ml-3">{item.label}</span>
                )}
              </Button>
            );

            return isSidebarCollapsed ? (
              <Tooltip key={item.id} content={item.label} placement="right">
                {MenuButton}
              </Tooltip>
            ) : (
              MenuButton
            );
          })}
        </nav>
      </div>
      <div className="flex-1 flex flex-col">
        <header className="bg-white border-b border-gray-200 px-6 py-4 shadow-sm">
          <div className="flex items-center justify-between">
            <div className="flex items-center">
              <Icon
                icon="heroicons:squares-2x2"
                className="w-8 h-8 text-green-600 mr-3"
              />
            </div>
            <div className="flex items-center space-x-4">
              {!isLoggedIn ? (
                <Button
                  color="success"
                  variant="solid"
                  onPress={handleLogin}
                  startContent={
                    <Icon
                      icon="heroicons:arrow-right-on-rectangle"
                      className="w-4 h-4"
                    />
                  }
                >
                  Iniciar Sesión
                </Button>
              ) : (
                <Dropdown placement="bottom-end">
                  <DropdownTrigger>
                    <div className="flex items-center cursor-pointer hover:bg-gray-50 rounded-lg p-2 transition-colors">
                      <Avatar
                        src={user.avatar}
                        name={user.name}
                        size="sm"
                        className="mr-2"
                      />
                      <div className="text-left hidden sm:block">
                        <p className="text-sm font-medium text-gray-900">
                          {user.name}
                        </p>
                        <p className="text-xs text-gray-500">{user.email}</p>
                      </div>
                      <Icon
                        icon="heroicons:chevron-down"
                        className="w-4 h-4 ml-2 text-gray-400"
                      />
                    </div>
                  </DropdownTrigger>
                  <DropdownMenu aria-label="User Actions">
                    <DropdownItem
                      key="profile"
                      startContent={
                        <Icon icon="heroicons:user" className="w-4 h-4" />
                      }
                    >
                      Mi Perfil
                    </DropdownItem>
                    <DropdownItem
                      key="settings"
                      startContent={
                        <Icon
                          icon="heroicons:cog-6-tooth"
                          className="w-4 h-4"
                        />
                      }
                    >
                      Configuración
                    </DropdownItem>
                    <DropdownItem
                      key="help"
                      startContent={
                        <Icon
                          icon="heroicons:question-mark-circle"
                          className="w-4 h-4"
                        />
                      }
                    >
                      Ayuda
                    </DropdownItem>
                    <DropdownItem
                      key="logout"
                      color="danger"
                      startContent={
                        <Icon
                          icon="heroicons:arrow-left-on-rectangle"
                          className="w-4 h-4"
                        />
                      }
                      onClick={handleLogout}
                    >
                      Cerrar Sesión
                    </DropdownItem>
                  </DropdownMenu>
                </Dropdown>
              )}
            </div>
          </div>
        </header>
        <main className="flex-1 p-6 overflow-y-auto bg-gray-50">
          <div className="max-w-7xl mx-auto">{children}</div>
        </main>
      </div>
    </div>
  );
};

export default Layout;
